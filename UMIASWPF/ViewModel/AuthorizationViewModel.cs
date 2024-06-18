using BingingLibrary;
using System.Windows;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;

namespace UMIASWPF.ViewModel
{
    public class AuthorizationViewModel: ApiHelper
    {
        #region obj
        private string _OMS;

        public string OMS
        {
            get => _OMS;
            set => SetField(ref _OMS, value);
        }

        private int _ID;
        public int ID
        {
            get => _ID;
            set => SetField(ref _ID, value);
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set => SetField(ref _Password, value);
        }

        #endregion
        #region commands
        public BindableCommand AuthPatient { get; set; }
        public BindableCommand AuthDoctorOrAdmin { get; set; }
        #endregion

        public event EventHandler ToDoctor;
        public event EventHandler ToAdmin;
        public event EventHandler ToPatient;

        public AuthorizationViewModel() 
        {
            AuthPatient = new BindableCommand(_ => _ = _AuthPatient());
            AuthDoctorOrAdmin = new BindableCommand(_ => _ = _AuthDoctorOrAdmin());

        }

        private async Task _AuthPatient()
        {
            try
            {
                if (OMS != null)
                {
                    var patient = Get<Patient>("Patients", Convert.ToInt32(OMS));
                    if (patient != null)
                    {
                        ToPatient?.Invoke(this, EventArgs.Empty);
                        Settings.Default.Patient = Convert.ToInt32(OMS);
                        Settings.Default.Save();
                    } else
                    {
                        MessageBox.Show("Данные введены не корректно");
                    }
                } else
                {
                    MessageBox.Show("Сначала введите номер полиса");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public async Task _AuthDoctorOrAdmin()
        {
            try
            {
                if (ID != 0 && Password != null)
                {
                    var doctor = Get<DoctorModel>("Doctors", ID);
                    var admin = Get<Admin>("Admins", ID);
                    if (doctor.EnterPassword == Password)
                    {
                        ToDoctor?.Invoke(this, EventArgs.Empty);
                        Settings.Default.Doctor = ID;
                        Settings.Default.Save();
                    }
                    else if (admin.EnterPassword == Password)
                    {
                        ToAdmin?.Invoke(this, EventArgs.Empty);
                        Settings.Default.Admin = ID;
                        Settings.Default.Save();
                    } else
                    {
                        MessageBox.Show("Данные введены не корректно");
                    }
                } else
                {
                    MessageBox.Show("Сначала заполните все поля");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
