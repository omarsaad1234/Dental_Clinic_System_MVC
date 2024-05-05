using AutoMapper;
using Dental_Clinic.Dtos.CreateAndEditRequests;
using Dental_Clinic.Dtos.GetResponse;
using Dental_Clinic.Models;

namespace Dental_Clinic.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<PatientDtoRequest, Patient>();
            CreateMap<AppointmentDtoRequest, Appointment>();
            CreateMap<AppointmentDtoEdit, Appointment>();
            CreateMap<Appointment, AppointmentDtoEdit>();
            CreateMap<InvoiceDtoRequest, Invoice>()
                .ForMember(dest=>dest.RemainAmount,src=>src.MapFrom(x=>(x.TotalCost)-(x.PaidAmount)))
                .ForMember(dest=>dest.RemainSessions,src=>src.MapFrom(x=>(x.TotalSessions)-(x.DoneSessions)));
            CreateMap<Invoice, InvoiceDtoEdit>();
            CreateMap<InvoiceDtoEdit, Invoice>()
                 .ForMember(dest => dest.RemainAmount, src => src.MapFrom(x => (x.TotalCost) - (x.PaidAmount)))
                .ForMember(dest => dest.RemainSessions, src => src.MapFrom(x => (x.TotalSessions) - (x.DoneSessions)));
            CreateMap<MedHistDtoRequest, MedicalHistory>();
            CreateMap<MedicalHistory, MedHistDtoEdit>();
            CreateMap<MedHistDtoEdit, MedicalHistory > ();
            CreateMap<DentalHistory, DentalHistoryDtoResponse>()
                .ForMember(dest => dest.Patient, src => src.MapFrom(x => x.Patient.Name))
                .ForMember(dest => dest.Mobile, src => src.MapFrom(x => x.Patient.Mobile));
            CreateMap<DentalDto, DentalHistory>();
        }
    }
}
