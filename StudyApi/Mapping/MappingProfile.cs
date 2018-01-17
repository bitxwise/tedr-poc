using AutoMapper;
using StudyApi.Controllers.Resources;
using StudyApi.Events;
using StudyApi.Models;

namespace StudyApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Study, StudyResource>()
                .ForMember(sr => sr.Facility, opt => opt.MapFrom(s => new NamedResource() {
                    Id = s.FacilityId, Name = s.FacilityName
                }))
                .ForMember(sr => sr.Procedure, opt => opt.MapFrom(s => new NamedResource() {
                    Id = s.ProcedureId, Name = s.ProcedureName
                }))
                .ForMember(sr => sr.StudyStatus, opt => opt.MapFrom(s => new NamedResource() {
                    Id = s.StatusId, Name = s.StatusName
                }));

            CreateMap<StudyResource, Study>()
                // Cannot map this way because Study members are private without public setters
                // .ForMember(s => s.FacilityId, opt => opt.MapFrom(sr => sr.Facility.Id))
                // .ForMember(s => s.FacilityName, opt => opt.MapFrom(sr => sr.Facility.Name))
                // .ForMember(s => s.ProcedureId, opt => opt.MapFrom(sr => sr.Procedure.Id))
                // .ForMember(s => s.ProcedureName, opt => opt.MapFrom(sr => sr.Procedure.Name))
                // .ForMember(s => s.StatusId, opt => opt.MapFrom(sr => sr.StudyStatus.Id))
                // .ForMember(s => s.StatusName, opt => opt.MapFrom(sr => sr.StudyStatus.Name))
                .AfterMap((sr, s) => {
                    s.Apply(new StudyCreatedEvent(sr.Id));
                    s.Apply(new AccessionNumberChangedEvent(sr.Id, sr.AccessionNumber));
                    s.Apply(new FacilityChangedEvent(sr.Id, sr.Facility.Id, sr.Facility.Name));
                    s.Apply(new ProcedureChangedEvent(sr.Id, sr.Procedure.Id, sr.Procedure.Name));
                    s.Apply(new StudyStatusChangedEvent(sr.Id, sr.StudyStatus.Id, sr.StudyStatus.Name));
                });
        }
    }
}