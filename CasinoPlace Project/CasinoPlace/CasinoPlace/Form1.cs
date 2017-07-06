using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CasinoPlace
{
    public partial class frmMain : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds; DataTable dt;
        SqlCommandBuilder builder;
        String qwery = "";
        String place1 = "";
        String conString = @"Data Source=(local);Initial Catalog=CasinoPlace;Integrated Security=True  ";
        int placeNum = 0;
        Button selectedButton = new Button();

        public frmMain()
        {
            InitializeComponent();
            tbxPass.PasswordChar = '*';
            tbxPass.MaxLength = 14;
            ChangeColor();


            gbxPlaceMap.Controls.AddRange(new Control[] { btnPlace23, btnPlace22, btnPlace21, btnPlace20, btnPlace19, btnPlace18, btnPlace17,
            btnPlace16, btnPlace15, btnPlace14, btnPlace13, btnPlace12, btnPlace11, btnPlace10,btnPlace9, btnPlace8, btnPlace7, btnPlace6,
            btnPlace5, btnPlace4, btnPlace3, btnPlace2, btnPlace1});

          /* foreach (Control c in gbxPlaceMap.Controls)
            {
                if (c is Button)
                { ((Button)c).Enabled = false; }
            } */

            // lblWelcomeLogIn.Visible = false;
            // lblLogInUser.Visible = false;

            grbLogOut.Visible = false;

            // btnLogOut.Visible = false;
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ChangeColor()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = con; con.Open();

                        int spacesLeftSL = 0; int spacesLeftRO = 0; int spacesLeftBJ = 0; int spacesLeftPK = 0;

                        for (int i = 1; i <= 23; i++)
                        {
                            int statusPlace1 = 1;
                            place1 = @"select AV_ID from PLACE where PLACE_ID ='" + i + "'";
                            string spacesLeft = @"select SPACES_LEFT from PLACE where PLACE_ID ='" + i + "'";
                            string spacesLeftSlots = @"select SPACES_LEFT from PLACE where PLACE_ID ='" + i + "' and AV_ID = 1 and GAME_TYPE_ID = 1";
                            string spacesLeftRoulette = @"select SPACES_LEFT from PLACE where PLACE_ID ='" + i + "' and AV_ID = 1 and GAME_TYPE_ID = 2";
                            string spacesLeftBlackjack = @"select SPACES_LEFT from PLACE where PLACE_ID ='" + i + "' and AV_ID = 1 and GAME_TYPE_ID = 3";
                            string spacesLeftPoker = @"select SPACES_LEFT from PLACE where PLACE_ID ='" + i + "' and AV_ID = 1 and GAME_TYPE_ID = 4";
                            comm.CommandText = place1;

                            SqlCommand ColorPlace1 = new SqlCommand(place1, con);
                            statusPlace1 = Convert.ToInt32(ColorPlace1.ExecuteScalar());
                            SqlCommand SpaceLeft = new SqlCommand(spacesLeft, con);
                            int spacesLeft1 = Convert.ToInt32(SpaceLeft.ExecuteScalar());
                            SqlCommand SpaceLeftSlots = new SqlCommand(spacesLeftSlots, con);
                            int spacesLeftSlots1 = Convert.ToInt32(SpaceLeftSlots.ExecuteScalar());
                            SqlCommand SpaceLeftRoulette = new SqlCommand(spacesLeftRoulette, con);
                            int spacesLeftRoulette1 = Convert.ToInt32(SpaceLeftRoulette.ExecuteScalar());
                            SqlCommand SpaceLeftBlackjack = new SqlCommand(spacesLeftBlackjack, con);
                            int spacesLeftBlackjack1 = Convert.ToInt32(SpaceLeftBlackjack.ExecuteScalar());
                            SqlCommand SpaceLeftPoker = new SqlCommand(spacesLeftPoker, con);
                            int spacesLeftPoker1 = Convert.ToInt32(SpaceLeftPoker.ExecuteScalar());

                            spacesLeftSL = spacesLeftSL + spacesLeftSlots1;
                            lblFreeSlotsNo.Text = "" + spacesLeftSL + "";
                            spacesLeftRO = spacesLeftRO + spacesLeftRoulette1;
                            lblFreeRouletteNo.Text = "" + spacesLeftRO + "";
                            spacesLeftBJ = spacesLeftBJ + spacesLeftBlackjack1;
                            lblFreeBlackjackNo.Text = "" + spacesLeftBJ + "";
                            spacesLeftPK = spacesLeftPK + spacesLeftPoker1;
                            lblFreePokerNo.Text = "" + spacesLeftPK + "";

                            foreach (var btn in gbxPlaceMap.Controls.OfType<Button>().Where(x => x.Name == "btnPlace" + i))

                                if (statusPlace1 == 1)
                                {
                                    btn.BackColor = Color.LimeGreen;
                                    btn.Text = "Free: " + spacesLeft1 + "";
                                    btn.Enabled = true;
                                }
                                else if (statusPlace1 == 2)
                                {
                                    btn.BackColor = Color.Gold;
                                    btn.Enabled = false;
                                    btn.Text = "Free: " + spacesLeft1 + "";
                                }
                                else if (statusPlace1 == 3)
                                {
                                    btn.BackColor = Color.Red;
                                    btn.Enabled = false;
                                    btn.Text = "Free: " + spacesLeft1 + "";
                                }
                                else
                                {
                                    btn.BackColor = Color.DimGray;
                                    btn.Enabled = false;
                                    btn.Text = "Out";
                                }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error!");
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void cbxSlots_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSlots.Checked == true)
            {
                cbxRoulette.Checked = false;
                cbxBlackjack.Checked = false;
                cbxPoker.Checked = false;

                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = con; con.Open();

                            for (int i = 1; i <= 23; i++)
                            {
                                int statusPlace2 = 1;
                                string place2 = @"select GAME_TYPE_ID from PLACE where PLACE_ID ='" + i + "'";
                                comm.CommandText = place2;

                                SqlCommand ColorPlace2 = new SqlCommand(place2, con);
                                statusPlace2 = Convert.ToInt32(ColorPlace2.ExecuteScalar());

                                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>().Where(x => x.Name == "btnPlace" + i))

                                    if (btn.Enabled == true)
                                    {
                                        if (statusPlace2 == 1)
                                        {
                                            btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                            btn.FlatAppearance.BorderSize = 4;
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                                        }
                                        else
                                        {
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                                        }
                                    }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error!");
                }
            }

            if (cbxSlots.Checked == false)
            {
                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>())
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;

            }
        }

        private void cbxRoulette_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRoulette.Checked == true)
            {
                cbxSlots.Checked = false;
                cbxBlackjack.Checked = false;
                cbxPoker.Checked = false;

                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = con; con.Open();

                            for (int i = 1; i <= 23; i++)
                            {
                                int statusPlace3 = 1;
                                string place3 = @"select GAME_TYPE_ID from PLACE where PLACE_ID ='" + i + "'";
                                comm.CommandText = place3;

                                SqlCommand ColorPlace2 = new SqlCommand(place3, con);
                                statusPlace3 = Convert.ToInt32(ColorPlace2.ExecuteScalar());

                                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>().Where(x => x.Name == "btnPlace" + i))
                                    if (btn.Enabled == true)
                                    {
                                        if (statusPlace3 == 2)
                                        {
                                            btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                            btn.FlatAppearance.BorderSize = 4;
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                                        }
                                        else
                                        {
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                                        }
                                    }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error!");
                }
            }

            if (cbxRoulette.Checked == false)
            {
                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>())
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            }
        }

        private void cbxBlackjack_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxBlackjack.Checked == true)
            {
                cbxSlots.Checked = false;
                cbxRoulette.Checked = false;
                cbxPoker.Checked = false;

                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = con; con.Open();

                            for (int i = 1; i <= 23; i++)
                            {
                                int statusPlace4 = 1;
                                string place4 = @"select GAME_TYPE_ID from PLACE where PLACE_ID ='" + i + "'";
                                comm.CommandText = place4;

                                SqlCommand ColorPlace2 = new SqlCommand(place4, con);
                                statusPlace4 = Convert.ToInt32(ColorPlace2.ExecuteScalar());

                                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>().Where(x => x.Name == "btnPlace" + i))
                                    if (btn.Enabled == true)
                                    {
                                        if (statusPlace4 == 3)
                                        {
                                            btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                            btn.FlatAppearance.BorderSize = 4;
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                                        }
                                        else
                                        {
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                                        }
                                    }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error!");
                }
            }

            if (cbxBlackjack.Checked == false)
            {
                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>())
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            }
        }

        private void cbxPoker_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxPoker.Checked == true)
            {
                cbxSlots.Checked = false;
                cbxRoulette.Checked = false;
                cbxBlackjack.Checked = false;

                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = con; con.Open();

                            for (int i = 1; i <= 23; i++)
                            {
                                int statusPlace5 = 1;
                                string place5 = @"select GAME_TYPE_ID from PLACE where PLACE_ID ='" + i + "'";
                                comm.CommandText = place5;

                                SqlCommand ColorPlace2 = new SqlCommand(place5, con);
                                statusPlace5 = Convert.ToInt32(ColorPlace2.ExecuteScalar());

                                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>().Where(x => x.Name == "btnPlace" + i))

                                    if (btn.Enabled == true)
                                    {
                                        if (statusPlace5 == 4)
                                        {
                                            btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                                            btn.FlatAppearance.BorderSize = 4;
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                                        }
                                        else
                                        {
                                            btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                                        }
                                    }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error!");
                }
            }

            if (cbxPoker.Checked == false)
            {
                foreach (var btn in gbxPlaceMap.Controls.OfType<Button>())
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            }
        }

        private void cbxLanguage_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxLanguage.Checked == true)
            {
                lblWelcome.Text = "Моля прокарайте картата си пред\r\n четеца и когато детайлите ви \r\nсе попълнят, натиснете 'Влез!'";
                btnLogIn.Text = "Влез!";
                grbLogIn.Text = "Влез";
                lblUser.Text = "Име:";
                lblPass.Text = "Код:";
                lblFreeSlots.Text = "Фрий\r\nМеста:\r\n";
                lblFreePoker.Text = "Фрий\r\nМеста:\r\n";
                lblFreeRoulette.Text = "Фрий\r\nМеста:\r\n";
                lblFreeBlackjack.Text = "Фрий\r\nМеста:\r\n";
                cbxSlots.Text = "Ротативки";
                cbxRoulette.Text = "Рулетка";
                cbxBlackjack.Text = "Блекджек";
                cbxPoker.Text = "Покер";
                cbxLanguage.Text = "English";
                btnLogOut.Text = "Излез!";
                grbLogOut.Text = "Излез";
                lblFilters.Text = "Обособете свободните маси от\r\nопределен тип чрез бутоните по-горе!";
                lblWelcomeLogIn.Text = "Добре дошли,";
            }
            if (cbxLanguage.Checked == false)
            {
                lblWelcome.Text = "Welcome! Please swipe your card in\r\nfront of the reader and once the \r\nlogin deta" +
    "ils are filled, click \'Log in!\'";
                btnLogIn.Text = "Log in!";
                grbLogIn.Text = "Log In";
                lblUser.Text = "User:";
                lblPass.Text = "Pass:";
                lblFreeSlots.Text = "Free\r\nPlaces:\r\n";
                lblFreePoker.Text = "Free\r\nPlaces:\r\n";
                lblFreeRoulette.Text = "Free\r\nPlaces:\r\n";
                lblFreeBlackjack.Text = "Free\r\nPlaces:\r\n";
                cbxSlots.Text = "Slots";
                cbxRoulette.Text = "Roulette";
                cbxBlackjack.Text = "Blackjack";
                cbxPoker.Text = "Poker";
                cbxLanguage.Text = "Български";
                btnLogOut.Text = "Log out!";
                grbLogOut.Text = "Log Out";
                lblFilters.Text = "Highlight the free tables from a\r\nparticular type by using the buttons above!";
                lblWelcomeLogIn.Text = "Welcome,";
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            PerformClick();
        }

        private void PerformClick()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from [USER] where Username like '" + tbxName.Text + "' and Password like '" + tbxPass.Text + "'", con);
            //SqlCommand cmd = new SqlCommand("select * from [USER] where Username like '" + textBox2.Text + "' and Password like '" + textBox1.Text + "'", con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count += 1;
            }

            if (count == 1)
            {
         
                ChangeColor();

                // lblWelcomeLogIn.Visible = true;
                // lblLogInUser.Visible = true;

                grbLogOut.Visible = true;

                //lblWelcome.Visible = false;
                //lblUser.Visible = false;
                //tbxName.Visible = false;
                //lblPass.Visible = false;
                // tbxPass.Visible = false;
                //cbxLanguage.Visible = false;
                // btnLogIn.Visible = false;

                //btnLogOut.Visible = true;



                grbLogIn.Visible = false;

                // select user name

                CurrentUser();

            }

            else
            {
                MessageBox.Show("Username or password are not correct!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            tbxName.Clear();
            tbxPass.Clear();

            con.Close();

            //https://www.youtube.com/watch?v=Ta1NtQReGnw
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            /* lblWelcome.Visible = true;
             lblUser.Visible = true;
             tbxName.Visible = true;
             lblPass.Visible = true;
             tbxPass.Visible = true;
             cbxLanguage.Visible = true;
             btnLogIn.Visible = true;
             btnLogOut.Visible = false; */

            grbLogIn.Visible = true;
            grbLogOut.Visible = false;

            ChangeColor();
          /*  foreach (Control c in gbxPlaceMap.Controls)
            {
                if (c is Button)
                { ((Button)c).Enabled = false; }
            } */
        }

        private void CurrentUser()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();

            String selectUserName = "select FNAME +' '+  LNAME from [USER] where Username like '" + tbxName.Text + "' and Password like '" + tbxPass.Text + "'";
            SqlCommand comUserName = new SqlCommand(selectUserName, con);
            String currentUser = Convert.ToString(comUserName.ExecuteScalar());
            lblLogInUser.Text = currentUser;

            con.Close();
        }

        private void ReservePlace()
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save a place on this table?", "Reserve a place", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlCommand comm = new SqlCommand())
                        {
                            comm.Connection = con;
                            con.Open();
                            int forID = placeNum;

                            String selectPlacesLeft = "select SPACES_LEFT from PLACE where PLACE_ID= '" + forID + "'";

                            SqlCommand cmdPlacesLeft = new SqlCommand(selectPlacesLeft, con);
                            int placesLeft = Convert.ToInt32(cmdPlacesLeft.ExecuteScalar());
                            
                            if (placesLeft > 0)
                            {
                                
                                String updatePlace = "update PLACE set SPACES_LEFT= SPACES_LEFT-1 where PLACE_ID= '" + forID + "'";
                                SqlCommand cmdPlaceNum = new SqlCommand(updatePlace, con);
                               cmdPlaceNum.ExecuteNonQuery();
                               // ChangeColor();
                                MessageBox.Show("Your place has been reserved!");
                                
                                int placesLeftRemain = placesLeft - 1;

                                if (placesLeftRemain == 0)
                                {
                                    String updateColor3 = @"update PLACE set AV_ID= 3 where PLACE_ID= '" + forID + "'";
                                    SqlCommand comUpdateCol3 = new SqlCommand(updateColor3, con);
                                    comUpdateCol3.ExecuteNonQuery();

                                    ChangeButtonColor();
                                    selectedButton.Text = "Free: "+ Convert.ToString(placesLeftRemain) ;
                                }
                                else
                                {
                                    if (placesLeftRemain==1)
                                    {
                                        String updateColor2 = @"update PLACE set AV_ID= 2 where PLACE_ID= '" + forID + "'";
                                        SqlCommand comUpdateCol2= new SqlCommand(updateColor2, con);
                                        comUpdateCol2.ExecuteNonQuery();

                                        ChangeButtonColor();
                                        selectedButton.Text = "Free: " + Convert.ToString(placesLeftRemain);
                                    }
                                    else
                                    {
                                        String updateColor1 = @"update PLACE set AV_ID= 1 where PLACE_ID= '" + forID + "'";
                                        SqlCommand comUpdateCol1 = new SqlCommand(updateColor1, con);
                                        comUpdateCol1.ExecuteNonQuery();

                                        ChangeButtonColor();
                                        selectedButton.Text = "Free: " + Convert.ToString(placesLeftRemain);
                                    }
                                }
                                
                                placeNum = 0;
                                
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("All the places have been reserved on this table");
                                placeNum = 0;
                                selectedButton.BackColor = Color.Red;

                                selectedButton.Enabled = false;

                               /* String updateColor = "update PLACE set AV_ID= 3 where PLACE_ID= '" + forID + "'";
                                SqlCommand cmdChangeColor = new SqlCommand(updateColor, con);
                                cmdChangeColor.ExecuteNonQuery();
                                */

                                ChangeColor();
                                MessageBox.Show("Your place has been reserved!");
                                
                                placeNum = 0;
                                
                                con.Close();
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                    placeNum = 0;
                }
            }
            else
            {
                MessageBox.Show("Another game?");
                placeNum = 0;
            }

            // con.Close();
        }


        private void ChangeButtonColor()
        {
            int btnColor = 0;
            int localPlace = placeNum;
            //String btnName = "btnPlace" + Convert.ToString(localPlace-1);

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand comColor = new SqlCommand())

                    {
                        comColor.Connection = con;
                        con.Open();
                        String selectColorNum = @"select AV_ID from PLACE where PLACE_ID='"+ localPlace + "' ";
                        comColor.CommandText = selectColorNum;
                        btnColor=Convert.ToInt32(comColor.ExecuteScalar());

                        if (btnColor == 3)
                        {
                            selectedButton.BackColor = Color.Red;
                           // selectedButton.Text = btnName;


                        }
                        else if (btnColor == 2)
                        {

                            selectedButton.BackColor = Color.Gold;
                           // selectedButton.Text = btnName;
                        }
                        else

                        {

                            selectedButton.BackColor = Color.LimeGreen;
                           // selectedButton.Text = btnName;
                        }


                    }

                }

               // con.Close();
            }
            catch
            {
                MessageBox.Show("Error");

            }
        }

     

        private void btnPlace15_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Text == "Free: 0")
            {
                clickedButton.BackColor = Color.Red;
                clickedButton.Enabled = false;
                MessageBox.Show("There are no places left on BJ 15 - the last one might have just been taken!");
            }
            else
            {
                if (clickedButton == null) // just to be on the safe side
                    return;

                selectedButton = clickedButton;

                string str = clickedButton.Name;

                string remStr = "btnPlace";
                string s = str.Replace(remStr, "");
                int btnID = Convert.ToInt32(s);
                placeNum = btnID;
                
                ReservePlace();
            }
        }

        private void btnPlace14_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Text == "Free: 0")
            {
                clickedButton.BackColor = Color.Red;
                clickedButton.Enabled = false;
                MessageBox.Show("There is no places left");
            }
            else
            {
                if (clickedButton == null) // just to be on the safe side
                    return;

                selectedButton = clickedButton;

                string str = clickedButton.Name;

                string remStr = "btnPlace";
                string s = str.Replace(remStr, "");
                int btnID = Convert.ToInt32(s);
                placeNum = btnID;

                ReservePlace();
            }
        }

        private void grbLogOut_Enter(object sender, EventArgs e)
        {

        }
    }
}
