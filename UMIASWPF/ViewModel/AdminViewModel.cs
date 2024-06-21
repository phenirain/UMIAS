using BingingLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UMIASApp.View;
using UMIASApp.View.Pages;
using UMIASWPF.Model;
using UMIASWPF.Utilities;

namespace UMIASWPF.ViewModel
{

    public class AdminViewModel: ApiHelper
    {

        #region objects
        private string _SelectedItem;
        public string SelectedItem
        {
            get => _SelectedItem;
            set => SetField(ref _SelectedItem, value);
        }

        private AdminModel _Admin;
        public AdminModel Admin
        {
            get => _Admin;
            set => SetField(ref _Admin, value);
        }

        private DoctorModel _Doctor;
        public DoctorModel Doctor
        {
            get => _Doctor;
            set => SetField(ref _Doctor, value);
        }

        private PatientModel _Patient;
        public PatientModel Patient
        {
            get => _Patient;
            set => SetField(ref _Patient, value);
        }

        private int _selectedIndex;
        #endregion

        #region collections
        public List<string> Roles { get; set; }
        
        public List<Speciality>? Specialities { get; set; }

        private ObservableCollection<object>? _items;
        public ObservableCollection<object>? Items
        {
            get => _items;
            set => SetField(ref _items, value);
        }


        #endregion


        public AdminViewModel()
        {
            Roles = new List<string>() { "Пациент", "Врач", "Администратор" };
            SelectedItem = Roles[0];
            Specialities = Get<List<Speciality>>("Specialities");
            Items = Get<ObservableCollection<object>>("Patients");
            Patient = new PatientModel();
            Doctor = new DoctorModel();
            Admin = new AdminModel();
        }

        public void SelectionRole()
        {
            if (SelectedItem == "Пациент")
            {
                Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault()!.SelectionFrame.Content = new UserPage(this);
                Items = Get<ObservableCollection<object>>("Patients");
            }
            else if (SelectedItem == "Врач")
            {
                Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault()!.SelectionFrame.Content = new DoctorPage(this);
                Items = Get<ObservableCollection<object>>("Doctors");
            }
            else
            {
                Application.Current.Windows.OfType<AdminWindow>().FirstOrDefault()!.SelectionFrame.Content = new AdminPage(this);
                Items = Get<ObservableCollection<object>>("Admins");
            }
        }

        public void SelectionSpeciality(object sender, RoutedEventArgs e)
        {
            if (sender is ComboBox cmb)
            {
                if (cmb.SelectedItem != null)
                {
                    Doctor.SpecialityId = (cmb.SelectedItem as Speciality).IdSpeciality;
                }
            }
        }

        public void SelectionDataGrid(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dg)
            {
                if (dg.SelectedItem != null)
                {
                    _selectedIndex = dg.SelectedIndex;
                    if (SelectedItem == "Пациент")
                        Patient = JsonConvert.DeserializeObject<PatientModel>(dg.SelectedItem.ToString());
                    else if (SelectedItem == "Врач")
                    {
                        Doctor = JsonConvert.DeserializeObject<DoctorModel>(dg.SelectedItem.ToString());
                    }
                    else
                        Admin = JsonConvert.DeserializeObject<AdminModel>(dg.SelectedItem.ToString());
                }
            }
        }

        public void Add()
        {
            string json;
            bool response = false;
            switch (SelectedItem)
            {
                case "Пациент":
                    json = JsonConvert.SerializeObject(Patient);
                    response = Post(json, "Patients");
                    break;
                case "Врач":
                    json = JsonConvert.SerializeObject(Doctor);
                    response = Post(json, "Doctors");
                    break;
                case "Администратор":
                    json = JsonConvert.SerializeObject(Admin);
                    response = Post(json, "Admins");
                    break;
            }
            if (response) SelectionRole();
        }

        public void Update()
        {
            string json;
            bool response = false;
            switch (SelectedItem)
            {
                case "Пациент":
                    json = JsonConvert.SerializeObject(Patient);
                    response = Put(json, "Patients", Patient.Oms);
                    break;
                case "Врач":
                    json = JsonConvert.SerializeObject(Doctor);
                    response = Put(json, "Doctors", Doctor.IdDoctor);
                    break;
                case "Администратор":
                    json = JsonConvert.SerializeObject(Admin);
                    response = Put(json, "Admins", (long)Admin.IdAdmin);
                    break;
            }
            if (response) SelectionRole();
        }

        public void Delete()
        {
            string json;
            bool response = false;
            switch (SelectedItem)
            {
                case "Пациент":
                    json = JsonConvert.SerializeObject(Patient);
                    response = Delete("Patients", Patient.Oms);
                    break;
                case "Врач":
                    json = JsonConvert.SerializeObject(Doctor);
                    response = Delete("Doctors", Doctor.IdDoctor);
                    break;
                case "Администратор":
                    json = JsonConvert.SerializeObject(Admin);
                    response = Delete("Admins", (long)Admin.IdAdmin);
                    break;
            }
            if (response) SelectionRole();
        }
    }
}
