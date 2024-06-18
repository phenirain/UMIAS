using BingingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;
using System.Collections.ObjectModel; // �������� ��� ��������� ��� ObservableCollection
using System.ComponentModel; // �������� ��� ���������, ���� �� ����������� INotifyPropertyChanged
using UMIASWPF.Utilities;

namespace UMIASWPF.ViewModel
{
	public class MedicalAppointmentViewModel : ApiHelper
	{
		//public ObservableCollection<Appointment> Appointments { get; set; }

		//public MedicalAppointmentsCardViewModel()
		//{
		//	Appointments = new ObservableCollection<Appointment>();
		//	LoadAppointments();
		//}

		//private async void LoadAppointments()
		//{
		//	var appointmentsData = await ApiHelper.Get<List<Appointment>>("appointments");
		//	foreach (var appointment in appointmentsData)
		//	{
		//		Appointments.Add(appointment);
		//	}
		//}

		//public event PropertyChangedEventHandler PropertyChanged;
		//protected virtual void OnPropertyChanged(string propertyName)
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		//}

		#region MyRegion

		private string _appointmentName;

		public string AppointmentName
		{
			get => _appointmentName;
			set => SetField(ref _appointmentName, value);
		}

		private string _address;

		public string Address
		{
			get => _address;
			set => SetField(ref _address, value);
		}

		private string _doctor;

		public string Doctor
		{
			get => _doctor;
			set => SetField(ref _doctor, value);
		}

		private string _date;

		public string Date
		{
			get => _date;
			set => SetField(ref _date, value);
		}

		public FlowDocument RTB { get; set; }

		private ObservableCollection<Appointments_Control> _elements = new();

		public ObservableCollection<Appointments_Control> Elements
		{
			get => _elements;
			set => SetField(ref _elements, value);
		}

		private long _oms;

		private int _id;

		public AppointmentViewModel()
		{
			var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
			_oms = (window.PatientsComboBox.SelectedItem as Patient).Oms;
			window.WindowTextBlock.Text = "�����";
			RTB = new();
			LoadCards();
		}
		#endregion
		private void LoadCards()
		{
			var appointments = ApiHelper.Get<List<Appointment>>("Appointments")!.Where(item => item.Oms == _oms).OrderBy(item => item.AppointmentDate).ToList();
			foreach (var appointment in appointments)
			{
				var researchDocument =
					ApiHelper.Get<ResearchDocument>("AppointmentDocuments", (long)appointment.IdAppointment!);
				if (researchDocument != null)
				{
					var doctor = ApiHelper.Get<Doctor>("Doctors", (long)appointment.DoctorId!);
					var card = new Appointments_Control(researchDocument.DocumentName, $"{doctor!.Surname} {doctor.FirstName.Substring(0, 1)}. {doctor.Patronymic.Substring(0, 1)}.", appointment.AppointmentDate.ToString("dd MMMM yyyy"), doctor.WorkAddress, (int)appointment.IdAppointment);
					card.Click += (sender, args) => LoadInfo(sender, args);
					Elements.Add(card);
				}
			}
		}

		private void LoadInfo(object sender, EventArgs args)
		{
			var card = sender as Appointments_Control;
			_id = card.IdAppointment;
			AppointmentName = card.NameResearch;
			Doctor = card.FIO;
			Address = card.Address;
			Date = card.Date;
			var document = ApiHelper.Get<ResearchDocument>("AppointmentDocuments", card.IdAppointment);
			File.WriteAllText("buffer.rtf", document.Rtf);
			var range = new TextRange(RTB.ContentStart, RTB.ContentEnd);
			var fs = new FileStream("buffer.rtf", FileMode.Open);
			range.Load(fs, DataFormats.Rtf);
			fs.Close();
			File.Delete("buffer.rtf");
		}

	}
}