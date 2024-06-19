using BingingLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        }

        public void SelectionRole()
        {
            if (SelectedItem == "Пациент")
                Items = Get<ObservableCollection<object>>("Patients");
            else if (SelectedItem == "Врач")
                Items = Get<ObservableCollection<object>>("Doctors");
            else
                Items = Get<ObservableCollection<object>>("Admins");
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
    }
}
