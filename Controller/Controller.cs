using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;

namespace Controller
{
    public class Controller
    {
        Form2 view2;
        Form1 view;
        Model1 model;
        List<contacte> listaContactes;
        List<contacte> contactesComplerts;

        public Controller()
        {
            view2 = new Form2();
            view = new Form1();
            model = new Model1();
            InicialitzarDatagrids();
            InicialitzarListeners();
            Application.Run(view);
        }

        public void InicialitzarListeners()
        {
            view.dgvContactes.SelectionChanged += dgvContactesSelectionChanged;
            view.dgvEmails.SelectionChanged += DgvEmails_SelectionChanged;
            view.dgvTelefons.SelectionChanged += DgvTelefons_SelectionChanged;

            view.buttonAfegirContactes.Click += ButtonAfegirContactes_Click;
            view.buttonAfegirTelefons.Click += ButtonAfegirTelefons_Click;
            view.buttonAfegirEmails.Click += ButtonAfegirEmails_Click;
            view.buttonModificarContactes.Click += ButtonModificarContactes_Click;
            view.buttonModificarTelefons.Click += ButtonModificarTelefons_Click;
            view.buttonModificarEmails.Click += ButtonModificarEmails_Click;
            view.buttonEsborrarContactes.Click += ButtonEsborrarContactes_Click;
            view.buttonEsborrarTelefons.Click += ButtonEsborrarTelefons_Click;
            view.buttonEsborrarEmails.Click += ButtonEsborrarEmails_Click;

            view.btAfegirContacteComplert.Click += BtAfegirContacteComplert_Click;

            view.btBuscarId.Click += BtBuscarId_Click;
            view.btBuscarNom.Click += BtBuscarNom_Click;
            view.btBuscarTelefon.Click += BtBuscarTelefon_Click;
            view.btBuscarEmail.Click += BtBuscarEmail_Click;

            view2.btAfegirContacteComplertForm2.Click += BtAfegirContacteComplertForm2_Click;

        }

        private void DgvTelefons_SelectionChanged(object sender, EventArgs e)
        {
            if (view.dgvTelefons.SelectedRows.Count > 0)
            {
                view.tbTelefon.Text = view.dgvTelefons.SelectedRows[0].Cells["telefon1"].Value.ToString();
                view.tbTipus.Text = view.dgvTelefons.SelectedRows[0].Cells["tipus"].Value.ToString();
            }
            else
            {
                view.tbTelefon.Text = "";
                view.tbTipus.Text = "";
            }
        }

        private void DgvEmails_SelectionChanged(object sender, EventArgs e)
        {
            if (view.dgvEmails.SelectedRows.Count > 0)
            {
                try
                {
                    view.tbEmail.Text = view.dgvEmails.SelectedRows[0].Cells["email1"].Value.ToString();
                    view.tbTipusE.Text = view.dgvEmails.SelectedRows[0].Cells["tipus"].Value.ToString();
                } catch(Exception ex)
                {
                    view.tbEmail.Text = "";
                    view.tbTipusE.Text = "";
                }
            }
            else
            {
                view.tbEmail.Text = "";
                view.tbTipusE.Text = "";
            }
        }

        private void BtAfegirContacteComplertForm2_Click(object sender, EventArgs e)
        {
            contacte c = new contacte();
            c.nom = view2.tbNom2.Text;
            c.cognoms = view2.tbCognoms2.Text;
            c.telefons = new List<telefon>();
            c.emails = new List<email>();

            c = model.InsertContacte(c);
            InicialitzarDatagrids();

            telefon t = new telefon();
            t.telefon1 = view2.tbTelefon2.Text;
            t.tipus = view2.tbTipus2.Text;
            t.contacteId = c.contacteId;

            email em = new email();
            em.email1 = view2.tbEmail2.Text;
            em.tipus = view2.tbTipusE2.Text;
            em.contacteId = c.contacteId;

            model.InsertTelefon(t);
            model.InsertEmail(em);

            view2.Visible = false;
            InicialitzarDatagrids();
        }

        private void BtAfegirContacteComplert_Click(object sender, EventArgs e)
        {
            view2.Visible = true;
            //view2.ShowDialog();
        }

        private void BtBuscarEmail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(view.tbBuscarEmail.Text))
            {
                listaContactes = new List<contacte>();

                listaContactes = model.GetContactesByEmail(view.tbBuscarEmail.Text);
                view.dgvContactes.DataSource = listaContactes;
            }
            else
            {
                listaContactes = new List<contacte>();

                view.dgvContactes.DataSource = contactesComplerts.OrderBy(x => x.cognoms).ThenBy(x => x.nom).ToList();
            }
        }

        private void BtBuscarTelefon_Click(object sender, EventArgs e)
        {
            view.dgvTelefons.DataSource = new List<telefon>();
            if (!string.IsNullOrEmpty(view.tbBuscarTelefon.Text))
            {
                try
                {
                    listaContactes = model.GetContactesByTelefon(view.tbBuscarTelefon.Text);
                    view.dgvContactes.DataSource = listaContactes;
                }
                catch (Exception ex)
                {
                    listaContactes = new List<contacte>();
                }
            }
            else
            {
                listaContactes = new List<contacte>();
                view.dgvContactes.DataSource = contactesComplerts.OrderBy(x => x.cognoms).ThenBy(x => x.nom).ToList();
            }
        }

        private void BtBuscarNom_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(view.tbBuscarNom.Text))
            {
                listaContactes = new List<contacte>();
                listaContactes = model.GetContactesByName(view.tbBuscarNom.Text);
                try
                {
                    view.dgvContactes.DataSource = listaContactes.OrderBy(x => x.cognoms).ThenBy(x => x.nom).ToList();
                }
                catch (Exception ex)
                {
                    view.dgvContactes.DataSource = new List<contacte>();
                }
            }
            else
            {
                listaContactes = new List<contacte>();
                view.dgvContactes.DataSource = contactesComplerts.OrderBy(x => x.cognoms).ThenBy(x => x.nom).ToList();
            }
        }

        private void BtBuscarId_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(view.tbBuscarId.Text))
            {
                listaContactes = new List<contacte>();

                listaContactes.Add(model.GetContacteById(Convert.ToInt32(view.tbBuscarId.Text)));

                try
                {
                    view.dgvContactes.DataSource = listaContactes.OrderBy(x => x.cognoms).ThenBy(x => x.nom).ToList();
                }
                catch (Exception ex)
                {
                    view.dgvContactes.DataSource = new List<contacte>();
                    MessageBox.Show("No existeix un contacte amb aquest id");
                }
            }
            else
            {
                listaContactes = new List<contacte>();
                view.dgvContactes.DataSource = contactesComplerts.OrderBy(x => x.cognoms).ThenBy(x => x.nom).ToList();
            }
        }

        private void ButtonEsborrarTelefons_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(view.dgvTelefons.SelectedRows[0].Cells[0].Value);
            model.DeleteTelefon(id);
            InicialitzarDatagrids();
        }

        private void ButtonEsborrarContactes_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(view.dgvContactes.SelectedRows[0].Cells[0].Value);
            model.DeleteContacte(id);
            InicialitzarDatagrids();
        }

        private void ButtonEsborrarEmails_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(view.dgvEmails.SelectedRows[0].Cells[0].Value);
            model.DeleteEmail(id);
            InicialitzarDatagrids();
        }

        private void ButtonModificarEmails_Click(object sender, EventArgs e)
        {
            if (view.dgvEmails.SelectedRows.Count > 0)
            {
                email em = new email();
                int id = Convert.ToInt32(view.dgvEmails.SelectedRows[0].Cells[0].Value);
                em.emailId = id;
                em.email1 = view.tbEmail.Text;
                em.tipus = view.tbTipusE.Text;
                model.UpdateEmail(em);
                InicialitzarDatagrids();
            }
        }

        private void ButtonModificarTelefons_Click(object sender, EventArgs e)
        {
            if (view.dgvTelefons.SelectedRows.Count > 0)
            {
                telefon t = new telefon();
                int id = Convert.ToInt32(view.dgvTelefons.SelectedRows[0].Cells[0].Value);
                t.telId = id;
                t.telefon1 = view.tbTelefon.Text;
                t.tipus = view.tbTipus.Text;
                model.UpdateTelefon(t);
                InicialitzarDatagrids();
            }
        }

        private void ButtonModificarContactes_Click(object sender, EventArgs e)
        {
            if (view.dgvContactes.SelectedRows.Count > 0)
            {
                contacte c = new contacte();
                c.contacteId = Convert.ToInt32(view.dgvContactes.SelectedRows[0].Cells[0].Value);
                c.nom = view.tbNom.Text;
                c.cognoms = view.tbCognoms.Text;
                model.UpdateContacte(c);
                InicialitzarDatagrids();
            }
        }

        private void ButtonAfegirEmails_Click(object sender, EventArgs e)
        {
            int idContacte = Convert.ToInt32(view.dgvContactes.SelectedRows[0].Cells[0].Value);

            email em = new email();
            em.email1 = view.tbEmail.Text;
            em.tipus = view.tbTipusE.Text;
            em.contacteId = idContacte;
            model.InsertEmail(em);

            InicialitzarDatagrids();
        }

        private void ButtonAfegirTelefons_Click(object sender, EventArgs e)
        {
            int idContacte = Convert.ToInt32(view.dgvContactes.SelectedRows[0].Cells["contacteId"].Value);

            telefon te = new telefon();
            te.telefon1 = view.tbTelefon.Text;
            te.tipus = view.tbTipus.Text;
            te.contacteId = idContacte;
            model.InsertTelefon(te);

            InicialitzarDatagrids();
        }

        private void ButtonAfegirContactes_Click(object sender, EventArgs e)
        {
            contacte c = new contacte();
            c.nom = view.tbNom.Text;
            c.cognoms = view.tbCognoms.Text;
            c.telefons = new List<telefon>();
            c.emails = new List<email>();
            model.InsertContacte(c);

            InicialitzarDatagrids();
        }

        public void InicialitzarDatagrids()
        {
            List<contacte> contactes = model.GetContactes();
            contacte c = contactes.FirstOrDefault();
            List<telefon> telefons = contactes.Where(x => x.contacteId == c.contacteId).SingleOrDefault().telefons.ToList();
            List<email> emails = contactes.Where(x => x.contacteId == c.contacteId).SingleOrDefault().emails.ToList();

            view.dgvContactes.DataSource = contactes;
            view.dgvContactes.Columns["telefons"].Visible = false;
            view.dgvContactes.Columns["emails"].Visible = false;

            view.dgvTelefons.DataSource = telefons;

            view.dgvEmails.DataSource = emails;

        }

        public void dgvContactesSelectionChanged(object sender, EventArgs args)
        {
            //List<contacte> contactes = model.GetContactes();
            contacte c = (contacte)view.dgvContactes.CurrentRow.DataBoundItem;
            
            List<telefon> telefons = null;
            List<email> emails = null;
            try
            {
                telefons = c.telefons.ToList();
                emails = c.emails.ToList();
            } catch (Exception ex)
            {
                telefons = new List<telefon>();
                emails = new List<email>();
            }
            
            view.dgvTelefons.DataSource = telefons;
            view.dgvEmails.DataSource = emails;

            view.tbNom.Text = c.nom;
            view.tbCognoms.Text = c.cognoms;

        }
    }
}
