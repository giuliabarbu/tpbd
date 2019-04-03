using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenTPBD
{
    public partial class Form1 : Form
    {
      
        string connectionString = "Data Source=GIULIA-PC;Initial Catalog=Angajati;Integrated Security=True;MultipleActiveResultSets=True";
        public static string parolaModificare;
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";
        public Form1()
        {
            InitializeComponent();
            
            //tabControl1.Selected+= new TabControlEventHandler(tabControl1_Selected);
           // tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
        }

      

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage4)
            {
                getProcente();
                button2.Enabled = false;

                using (Parola par = new Parola(parolaModificare))
                {
                    if (par.ShowDialog() == DialogResult.OK)
                    {
                            button2.Enabled = true;
                    }
                }                            
               
            }

            if (tabControl1.SelectedTab == tabPage7)
            {
                Application.Exit();

            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabPage8)
            {
              // this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

            }
            if (tabControl2.SelectedTab == tabPage9)
            {
              // this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);

            }
        }

        
        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl4.SelectedTab == tabPage13)
            {
             //  this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);

            }
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
         
            this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);
         
            this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

            this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);
            
        }



        private void adaugaAngajatButton_Click(object sender, EventArgs e)
        {
            
            //NUME="+ adaugaNumeText.Text+"and PRENUME="+adaugaPrenumeText.Text +"
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                double CAS, CASS, IMPOZIT;
                int total_brut, cas_adaugat, cass_adaugat, brut_impozabil, impozit_adaugat, virat_card;
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Procente", connection))
                {
                    SqlDataReader dr;
                    dr=command1.ExecuteReader();
                    dr.Read();
                        CAS = Convert.ToDouble(dr["CAS"]);
                        CASS = Convert.ToDouble(dr["CASS"]);
                        IMPOZIT = Convert.ToDouble(dr["IMPOZIT"]);

                         total_brut = Convert.ToInt32(Convert.ToInt32(adaugaSalarText.Text) * (1 + Convert.ToDouble(adaugaSporText.Text) / 100)) + Convert.ToInt32(adaugaPremiiText.Text);
                         cas_adaugat = Convert.ToInt32(total_brut * CAS);
                         cass_adaugat = Convert.ToInt32(total_brut * CASS);
                         brut_impozabil = total_brut - cas_adaugat - cass_adaugat;
                         impozit_adaugat = Convert.ToInt32(brut_impozabil * IMPOZIT);
                         virat_card = total_brut - impozit_adaugat - cas_adaugat - cass_adaugat - Convert.ToInt32(adaugaRetineriText.Text);
                    

                }
                try
                {
                    using (SqlCommand command2 = new SqlCommand("INSERT INTO Salarii(NUME,PRENUME,FUNCTIE,SALARIU_BAZA,SPOR,PREMII_BRUTE,TOTAL_BRUT," +
                        "BRUT_IMPOZABIL,IMPOZIT,CAS,CASS,RETINERI,VIRAT_CARD) values('" + adaugaNumeText.Text + "','" + adaugaPrenumeText.Text + "','"
                        + adaugaFunctieText.Text + "'," + Convert.ToInt32(adaugaSalarText.Text) + ","
                        + Convert.ToInt32(adaugaSporText.Text) + "," + Convert.ToInt32(adaugaPremiiText.Text)
                        + "," + total_brut + "," + brut_impozabil + "," + impozit_adaugat + "," + cas_adaugat + "," + cass_adaugat
                        + "," + Convert.ToInt32(adaugaRetineriText.Text) + "," +virat_card + ")", connection))
                    {
                        var i = command2.ExecuteNonQuery();
                        MessageBox.Show("OK-adaugare efectuata - linii afectate" + i.ToString());
                        vizualizareAngajati.Items.Add(adaugaNumeText.Text + ", " + adaugaPrenumeText.Text + ", " + adaugaFunctieText.Text + ", " + adaugaSalarText.Text + ", " + adaugaSporText.Text + ", " + adaugaPremiiText.Text + ", " + total_brut + ", " + brut_impozabil + ", " + impozit_adaugat + ", " + cas_adaugat + ", " + cass_adaugat + ", " + adaugaRetineriText.Text + ", " + virat_card);
                        adaugaNumeText.Text = "";
                        adaugaPrenumeText.Text = "";
                        adaugaFunctieText.Text = "";
                        adaugaSporText.Text = "";
                        adaugaSalarText.Text = "";
                        adaugaRetineriText.Text = "";
                        adaugaPremiiText.Text = "";



                        this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);

                        this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

                        this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Conexiune esuata, incalcare constrangeri/ date eronate: " + ex.Message);
                }
                finally {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }

            }
        }

        private void adaugaSalarText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void adaugaSporText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void adaugaPremiiText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void adaugaRetineriText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void adaugaNumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void adaugaPrenumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void adaugaFunctieText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void cautareNumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }
        private void cautarePrenumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try {
                this.Validate();
                actualizareBindingSource.EndEdit();
                actualizareTableAdapter.Update(angajatiDataSet.Salarii);

                this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);

                this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

                this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            actualizareBindingSource.CancelEdit();
            angajatiDataSet.RejectChanges();
        }

        private void stergereNumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
            if ((e.KeyChar == '\r'))
                stergerePrenumeText.Focus();
        }

        private void cautarePentruStergereButton_Click(object sender, EventArgs e)
        {
            if ((stergereNumeText.Text.Length > 0) && (stergerePrenumeText.Text.Length > 0))
            {
                int loc1 = stergereBindingSource.Find("NUME", stergereNumeText.Text);
                int loc2 = stergereBindingSource.Find("PRENUME", stergerePrenumeText.Text);
                if ((loc1 != -1) && (loc2 != -1))
                {
                    if (loc1 == loc2)
                    {
                        mesajCautareStergere.ForeColor = System.Drawing.Color.Green;
                        mesajCautareStergere.Text = "Angajatul cautat exista in baza de date.";
                        stergereBindingSource.Position = loc1;
                    }
                }
                else
                {
                    mesajCautareStergere.ForeColor = System.Drawing.Color.Red;
                    mesajCautareStergere.Text = "Angajatul nu exista in baza de date. Incercati din nou!";
                    stergereBindingSource.Position = 0;
                }
            }
        }

        private void stergerePrenumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
            if ((e.KeyChar == '\r'))
                cautarePentruStergereButton_Click(null, null);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Doriti stergere?","Stergere", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (stergereBindingSource.Count > 0)
                {
                    stergereBindingSource.RemoveCurrent();
                    stergereBindingSource.EndEdit();

                   
                }
            }

            try
            {
                stergereTableAdapter.Update(angajatiDataSet1.Salarii);
                this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

                this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);


                this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                
            }
        }
        public void getProcente() {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command4 = new SqlCommand("SELECT * FROM Procente", connection))
                    {
                        SqlDataReader dr;
                        dr = command4.ExecuteReader();
                        dr.Read();
                        modificareCasText.Text = dr["CAS"].ToString();
                        modificareCassText.Text = dr["CASS"].ToString();
                        modificareImpozitText.Text = dr["IMPOZIT"].ToString();
                        parolaModificare = Decrypt(dr["PAROLA"].ToString());
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Conexiune esuata, incalcare constrangeri/ date eronate: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }


        public void getAngajat()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Salarii where NUME='"+cautareNumeText.Text+"' and PRENUME='"+cautarePrenumeText.Text+"'", connection))
                    {
                        SqlDataReader dr;
                        dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            cautareUpdateMesaj.ForeColor = System.Drawing.Color.Green;
                            cautareUpdateMesaj.Text = "Angajatul a fost gasit!";
                            updateNumeText.Text = dr["NUME"].ToString();
                            updatePrenumeText.Text = dr["PRENUME"].ToString();
                            updateFunctieText.Text = dr["FUNCTIE"].ToString();
                            updateSporText.Text = dr["SPOR"].ToString();
                            updateSalarText.Text = dr["SALARIU_BAZA"].ToString();
                            updateRetineriText.Text = dr["RETINERI"].ToString();
                            updatePremiiText.Text = dr["PREMII_BRUTE"].ToString();
                            
                        }
                        else {
                            cautareUpdateMesaj.ForeColor = System.Drawing.Color.Red;
                            cautareUpdateMesaj.Text = "Angajatul nu  a fost gasit!";
                            updateNumeText.Text = "";
                            updatePrenumeText.Text = "";
                            updateFunctieText.Text = "";
                            updateSporText.Text = "";
                            updateSalarText.Text = "";
                            updateRetineriText.Text = "";
                            updatePremiiText.Text = "";
                        }
                        
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Conexiune esuata, incalcare constrangeri/ date eronate: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand command4 = new SqlCommand("UPDATE Procente SET CAS=" + Convert.ToDouble(modificareCasText.Text) + ",CASS=" + Convert.ToDouble(modificareCassText.Text) + ",IMPOZIT=" + Convert.ToDouble(modificareImpozitText.Text), connection))
                        {
                            command4.ExecuteNonQuery();
                            mesajModificareProcente.ForeColor = System.Drawing.Color.Green;
                            mesajModificareProcente.Text = "Modificarea s-a efectuat cu succes!";
                        }
                    }
                    catch
                    {
                        mesajModificareProcente.ForeColor = System.Drawing.Color.Red;
                        mesajModificareProcente.Text = "Modificarea nu s-a putut efectua! Conexiune esuata, incalcare constrangeri/ date eronate";

                    this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);

                    this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

                    this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);
                }
                    finally
                    {
                        getProcente();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
        }
                

        public static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        private void cautareButton_Click(object sender, EventArgs e)
        {
            getAngajat();
        }

        private void actualizareAngajatButton_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                double CAS, CASS, IMPOZIT;
                int total_brut, cas_adaugat, cass_adaugat, brut_impozabil, impozit_adaugat, virat_card;
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Procente", connection))
                {
                    SqlDataReader dr;
                    dr = command1.ExecuteReader();
                    dr.Read();
                    CAS = Convert.ToDouble(dr["CAS"]);
                    CASS = Convert.ToDouble(dr["CASS"]);
                    IMPOZIT = Convert.ToDouble(dr["IMPOZIT"]);

                    total_brut = Convert.ToInt32(Convert.ToInt32(updateSalarText.Text) * (1 + Convert.ToDouble(updateSporText.Text) / 100)) + Convert.ToInt32(updatePremiiText.Text);
                    cas_adaugat = Convert.ToInt32(total_brut * CAS);
                    cass_adaugat = Convert.ToInt32(total_brut * CASS);
                    brut_impozabil = total_brut - cas_adaugat - cass_adaugat;
                    impozit_adaugat = Convert.ToInt32(brut_impozabil * IMPOZIT);
                    virat_card = total_brut - impozit_adaugat - cas_adaugat - cass_adaugat - Convert.ToInt32(updateRetineriText.Text);


                }

                try
                {
                    
                    using (SqlCommand command4 = new SqlCommand("UPDATE Salarii SET NUME='" + updateNumeText.Text + "',PRENUME='" + updatePrenumeText.Text + "',FUNCTIE='"+updateFunctieText.Text + "', SALARIU_BAZA=" + Convert.ToInt32(updateSalarText.Text) 
                        + ", SPOR="+ Convert.ToInt32(updateSporText.Text) + ",PREMII_BRUTE=" + Convert.ToInt32(updatePremiiText.Text)
                        + ",TOTAL_BRUT=" + total_brut + ",BRUT_IMPOZABIL=" + brut_impozabil + ",IMPOZIT=" + impozit_adaugat + ",CAS=" + cas_adaugat + ",CASS=" + cass_adaugat
                        + ",RETINERI=" + Convert.ToInt32(updateRetineriText.Text) + ",VIRAT_CARD=" + virat_card, connection))
                    {
                        command4.ExecuteNonQuery();
                        label25.ForeColor = System.Drawing.Color.Green;
                        label25.Text = "Modificarea s-a efectuat cu succes!";
                    }
                }
                catch
                {
                    label25.ForeColor = System.Drawing.Color.Red;
                    label25.Text = "Modificarea nu s-a putut efectua! Conexiune esuata, incalcare constrangeri/ date eronate";


                }
                finally
                {
                    
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        private void calculSalariiButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                double CAS, CASS, IMPOZIT;
                int total_brut, cas_adaugat, cass_adaugat, brut_impozabil, impozit_adaugat, virat_card;
                bool mesaj_calcule_efectuate=false;
                connection.Open();
                using (SqlCommand command1 = new SqlCommand("SELECT * FROM Procente", connection))
                {
                    SqlDataReader dr;
                    dr = command1.ExecuteReader();
                    dr.Read();
                    CAS = Convert.ToDouble(dr["CAS"]);
                    CASS = Convert.ToDouble(dr["CASS"]);
                    IMPOZIT = Convert.ToDouble(dr["IMPOZIT"]);



                }
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Salarii", connection))
                    {
                        SqlDataReader dr1;
                        dr1 = command.ExecuteReader();
                        while (dr1.Read())
                        {
                            total_brut = Convert.ToInt32(Convert.ToInt32(dr1["SALARIU_BAZA"].ToString()) * (1 + Convert.ToDouble(dr1["SPOR"].ToString()) / 100)) + Convert.ToInt32(dr1["PREMII_BRUTE"].ToString());
                            cas_adaugat = Convert.ToInt32(total_brut * CAS);
                            cass_adaugat = Convert.ToInt32(total_brut * CASS);
                            brut_impozabil = total_brut - cas_adaugat - cass_adaugat;
                            impozit_adaugat = Convert.ToInt32(brut_impozabil * IMPOZIT);
                            virat_card = total_brut - impozit_adaugat - cas_adaugat - cass_adaugat - Convert.ToInt32(dr1["RETINERI"].ToString());

                            try
                            {

                                using (SqlCommand command4 = new SqlCommand("UPDATE Salarii SET TOTAL_BRUT=" + total_brut + ",BRUT_IMPOZABIL=" + brut_impozabil + ",IMPOZIT=" + impozit_adaugat + ",CAS=" + cas_adaugat + ",CASS=" + cass_adaugat
                                     + ",VIRAT_CARD=" + virat_card + "where ID=" + Convert.ToInt32(dr1["ID"].ToString()), connection))
                                {
                                    command4.ExecuteNonQuery();
                                    mesaj_calcule_efectuate = true;
                                    
                                }
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("Eroare la efectuarea calculelor!" + ex.ToString());

                            }
                        }
                    }

                    if (mesaj_calcule_efectuate == true) {
                        this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);
                        MessageBox.Show("Calculele s-au efectuat cu succes!");

                        this.salariiTableAdapter.Fill(this.angajatiDataSet2.Salarii);

                        this.stergereTableAdapter.Fill(this.angajatiDataSet1.Salarii);

                        this.actualizareTableAdapter.Fill(this.angajatiDataSet.Salarii);
                    }                    
                }
                catch {
                    MessageBox.Show("Calculele nu s-au putut efectua! Conexiune esuata, incalcare constrangeri/ date eronate");
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
        }

        private void adaugaNumeText_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report rep = new Report();
            rep.Show();
        }

        private void updateNumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void updatePrenumeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void updateFunctieText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (!char.IsControl(e.KeyChar))  && (e.KeyChar != '-'))
                e.Handled = true;
        }

        private void updateSalarText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void updateSporText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void updateRetineriText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void updatePremiiText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void modificareCasText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void modificareCassText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void modificareImpozitText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) & (!char.IsControl(e.KeyChar)) && (e.KeyChar != '.'))
                e.Handled = true;
        }
    }
}

