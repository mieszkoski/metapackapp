using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaPack_App
{
    
    public partial class formPizzeria : Form
    {
        Decimal totalPrice = 0;
        public formPizzeria()
        {
            InitializeComponent();
            makePanelsInvisible();
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void uncheckCheckBoxes(Panel panel)
        {
            foreach (Control cBox in panel.Controls)
            {
                if (cBox is CheckBox)
                {
                    ((CheckBox)cBox).Checked = false;
                }
            }
        }
        private void makePanelsInvisible()
        {
            panelPizza.Visible = false;
            panelMainDishes.Visible = false;
            panelSoup.Visible = false;
            panelDrinks.Visible = false;
            panelCheckout.Visible = false;
            panelEmail.Visible = false;
            panelComment.Visible = false;
            panelSuccess.Visible = false;
            labelSuccessEmail.Visible = false;

            panelMain.Visible = true;
        }
        private void sendEmail(string fileName)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;

            SmtpServer.Credentials = new System.Net.NetworkCredential("bobinskibobo56@gmail.com", "metaPackApp113+");

            MailAddress fromMail = new MailAddress("bobinskibobo56@gmail.com","MetaPack Pizzeria");
            if (!IsValidEmail(textBoxEmail.Text)) return;
            MailAddress toMail = new MailAddress(textBoxEmail.Text, "Klient MetaPack Pizzeria");

            MailMessage mail = new MailMessage();
            mail.From = fromMail;
            mail.To.Add(toMail);
            mail.Subject = "Potwierdzenie zamówienia MetaPack Pizzeria.";
            mail.Body = "Łap twój paragon";

            Attachment attachment = new Attachment(fileName);
            mail.Attachments.Add(attachment);

            SmtpServer.SendCompleted += SmtpServer_SendCompleted;
            SmtpServer.SendMailAsync(mail);

            labelSuccessEmail.Text = "Potwierdzenie znajdziesz na \n"+toMail.Address;
        }

        private void SmtpServer_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                labelSuccessEmail.Visible = false;
                textBoxEmail.Text = "";
                return;
            }
            labelSuccessEmail.Visible = true;
        }

        private void addItem(Item item)
        {
            //ADD ITEM TO LISTBOX
            listBoxCheckout.Items.Add(item);

            //UPDATE THE PRICE
            totalPrice += item.price;
            textBoxTotal.Text = totalPrice.ToString()+"zł";

            //POP UP THE NOTIFICATION
            panelAddedNotification.Visible = true;
            Task.Delay(600).ContinueWith(_ =>
            {
                Invoke(new MethodInvoker(() => { panelAddedNotification.Visible = false; }));
            });
        }
        private void succesNotification()
        {
            panelSuccess.Visible = true;
            Task.Delay(5000).ContinueWith(_ =>
            {
                Invoke(new MethodInvoker(() => { panelSuccess.Visible = false; }));
            });
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
            //CHECK IF CHECKOUT EMPTY
            if (listBoxCheckout.Items.Count == 0) return;

            //SAVE FILE
            string sPath = "paragon" + DateTime.Now.ToString("yyyy'-'MM-''dd-HH'-'mm'-'ss'-'fffffff") + ".txt";


            StreamWriter raportFile = new StreamWriter(sPath);

            raportFile.WriteLine(DateTime.Now);

            foreach (Item item in listBoxCheckout.Items)
            {
                raportFile.WriteLine(item);
            }

            //COMMENT ABOUT ORDER
            if (!string.IsNullOrWhiteSpace(textBoxComment.Text))
            {
                raportFile.WriteLine("\nTwoja uwaga do zamówienia");
                raportFile.Write(textBoxComment.Text + '\n');
            }


            const string farewell = "\nDziękujemy i smacznego! \nMetaPack Pizzeria";
            raportFile.WriteLine(farewell);
            raportFile.Close();

            //SEND EMAIL
            sendEmail(sPath);

            //CLEAR CHECKOUT LIST AND COMMENT TEXT
            listBoxCheckout.Items.Clear();
            totalPrice = 0;
            textBoxTotal.Text = totalPrice.ToString() + "zł";
            textBoxComment.Text = "";

            //BACK OUT
            buttonBackCheckout.PerformClick();

            succesNotification();
        }

        //BACK BUTTONS
        private void buttonBackMainDishes_Click(object sender, EventArgs e)
        {
            makePanelsInvisible();
        }
        private void buttonBackSoup_Click(object sender, EventArgs e)
        {
            makePanelsInvisible();
        }
        private void buttonBackDrinks_Click(object sender, EventArgs e)
        {
            makePanelsInvisible();
        }
        private void buttonBackCheckout_Click(object sender, EventArgs e)
        {
            makePanelsInvisible();
        }
        private void buttonBackComment_Click(object sender, EventArgs e)
        {
            makePanelsInvisible();
        }
        private void buttonBackPizza_Click(object sender, EventArgs e)
        {
            makePanelsInvisible();

            foreach (Panel p in panelPizza.Controls.OfType<Panel>())
                uncheckCheckBoxes(p);
        }

        //CLICK BUTTONS (TO OPEN PANELS)
        private void buttonPizza_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelPizza.Visible = true;
        }
        private void buttonMainDishes_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelMainDishes.Visible = true;
        }

        private void buttonSoup_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelSoup.Visible = true;
        }

        private void buttonDrinks_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelDrinks.Visible = true;
        }

        private void buttonReview_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelComment.Visible = true;
        }
        private void buttonCheckout_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelCheckout.Visible = true;
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            panelEmail.Visible = true;
        }

        //CLICK BUTTONS (TO ADD ITEMS)
        private void buttonMargheritta_Click(object sender, EventArgs e)
        {
            addItem(new Margheritta(checkboxMargherittaCheese.Checked, checkboxMargherittaSalami.Checked, checkboxMargherittaHam.Checked, checkboxMargherittaMushroom.Checked));

            uncheckCheckBoxes(panelMargherittaExtras);
        }
        private void buttonVegetariana_Click(object sender, EventArgs e)
        {
            addItem(new Vegetariana(checkboxVegetarianaCheese.Checked, checkboxVegetarianaSalami.Checked, checkboxVegetarianaHam.Checked, checkboxVegetarianaMushrooms.Checked));

            uncheckCheckBoxes(panelVegetarianaExtras);
        }
        private void buttonTosca_Click(object sender, EventArgs e)
        {
            addItem(new Tosca(checkboxToscaCheese.Checked, checkboxToscaSalami.Checked, checkboxToscaHam.Checked, checkboxToscaMushroom.Checked));

            uncheckCheckBoxes(panelToscaExtras);
        }
        private void buttonVenecia_Click(object sender, EventArgs e)
        {
            addItem(new Venecia(checkboxVeneciaCheese.Checked, checkboxVeneciaSalami.Checked, checkboxVeneciaHam.Checked, checkboxVeneciaMushroom.Checked));

            uncheckCheckBoxes(panelVeneciaExtras);
        }
        private void buttonCoffee_Click(object sender, EventArgs e)
        {
            addItem(new Coffee());
        }
        private void buttonTee_Click(object sender, EventArgs e)
        {
            addItem(new Tee());
        }
        private void buttonCola_Click(object sender, EventArgs e)
        {
            addItem(new Cola());
        }
        private void buttonTomatoSoup_Click(object sender, EventArgs e)
        {
            addItem(new TomatoSoup());
        }
        private void buttonChickenSoup_Click(object sender, EventArgs e)
        {
            addItem(new ChickenSoup());
        }
        private void buttonSchnitzelFries_Click(object sender, EventArgs e)
        {
            addItem(new Schnitzel(Schnitzel.Extra.Fries));
        }

        private void buttonSchnitzelRice_Click(object sender, EventArgs e)
        {
            addItem(new Schnitzel(Schnitzel.Extra.Rice));
        }

        private void buttonSchnitzelPotato_Click(object sender, EventArgs e)
        {
            addItem(new Schnitzel(Schnitzel.Extra.Potato));
        }

        private void buttonFishAndChips_Click(object sender, EventArgs e)
        {
            addItem(new FishAndChips());
        }

        private void buttonHungarianCake_Click(object sender, EventArgs e)
        {
            addItem(new HungarianCake());
        }
    }
}