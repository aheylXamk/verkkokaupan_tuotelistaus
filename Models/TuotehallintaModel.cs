using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;



namespace ot8.Models {

    public class TuoteTieto {

        public int Id {get; set;}
        public string Vari {get; set;}
        public string Koko {get; set;}
        public decimal Hinta {get; set;}
        public int Saldo {get; set;}
    }

    public class Tuotehallinta {

        private string YhteysAsetukset {get;} = "server=localhost;user=root;password=;database=verkkokauppa;SslMode=none";

        public List<TuoteTieto> haeKaikki() {

            try {

                using (MySqlConnection yhteys = new MySqlConnection(this.YhteysAsetukset)) {

                    yhteys.Open();

                    string SqlLause = "SELECT * FROM tuotteet";

                    MySqlDataReader tulokset = new MySqlCommand(SqlLause, yhteys).ExecuteReader();

                    List<TuoteTieto> lista = new List<TuoteTieto>();

                    while (tulokset.Read()) {

                        int tulosId = tulokset.GetInt32("Id");
                        string tulosVari = tulokset.GetString("Vari");
                        string tulosKoko = tulokset.GetString("Koko");
                        decimal tulosHinta = tulokset.GetDecimal("Hinta");
                        int tulosSaldo = tulokset.GetInt32("Varastotilanne");

                        lista.Add(new TuoteTieto() {Id=tulosId, Vari=tulosVari, Koko=tulosKoko, Hinta=tulosHinta, Saldo=tulosSaldo});


                    }


                    return lista;
                    
                }

            } catch (Exception e) {

                throw new Exception("Tietojen hakemisessa tapahtui virhe", e);
            }
        }

        public void LisaaUusi(string uusiVari, string uusiKoko, decimal uusiHinta, int uusiSaldo) {

            try {

                using (MySqlConnection yhteys = new MySqlConnection(this.YhteysAsetukset)) {

                    yhteys.Open();

                    string SqlLause = "INSERT INTO tuotteet (Vari, Koko, Hinta, Varastotilanne) VALUES (?,?,?,?)";

                    MySqlCommand kysely = new MySqlCommand(SqlLause, yhteys);

                    kysely.Parameters.Add("@uusiVari", MySqlDbType.Text);
                    kysely.Parameters.Add("@uusiKoko", MySqlDbType.Text);
                    kysely.Parameters.Add("@uusiHinta", MySqlDbType.Decimal);
                    kysely.Parameters.Add("@uusiSaldo", MySqlDbType.Int16);

                    kysely.Parameters["@uusiVari"].Value = uusiVari;
                    kysely.Parameters["@uusiKoko"].Value = uusiKoko;
                    kysely.Parameters["@uusiHinta"].Value = uusiHinta;
                    kysely.Parameters["@uusiSaldo"].Value = uusiSaldo;

                    kysely.Prepare();

                    kysely.ExecuteNonQuery();
                    
                }

            } catch (Exception e) {

                throw new Exception("Tietojen hakemisessa tapahtui virhe", e);
            }
        }


    }

}