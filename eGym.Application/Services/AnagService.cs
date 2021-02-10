using AutoMapper;
using eGym.Application.DTO;
using eGym.Core.Domain;
using eGym.Core.SeedWork.NSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface IAnagService
    {
        Task<IQueryable<Anag_Master>> ListAsync(
            AnagFilters filters,
            SearchStringKindEnum searchStringKind = SearchStringKindEnum.Contains,
            bool includeAddresses = false,
            bool includeContacts = false,
            bool includeDocuments = false,
            bool includeAthletes = false,
            bool includeAthleteNavigation = false,
            bool includeSports = false);
        Task<Anag_Master> FindByAsync(
            int? id = null,
            string taxCode = null,
            string vatNo = null,
            int? userId = null,
            int? athleteId = null,
            SearchStringKindEnum searchStringKind = SearchStringKindEnum.Contains,
            bool includeAddresses = false,
            bool includeContacts = false,
            bool includeDocuments = false,
            bool includeAthletes = false,
            bool includeAthleteNavigation = false,
            bool includeSports = false);
        Task<Anag_Master> SaveAsync(AnagDTO dto = null, Anag_Master entity = null);
    }

    public class AnagService : IAnagService
    {
        readonly IAnagRepository _repository;
        readonly IMapper _mapper;

        public AnagService(
            IAnagRepository anagRepository,
            IMapper mapper)
        {
            _repository = anagRepository ?? throw new ArgumentNullException(nameof(anagRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Anag_Master> FindByAsync(
            int? id = null, 
            string taxCode = null, 
            string vatNo = null, 
            int? userId = null, 
            int? athleteId = null,
            SearchStringKindEnum searchStringKind = SearchStringKindEnum.Contains,
            bool includeAddresses = false,
            bool includeContacts = false,
            bool includeDocuments = false,
            bool includeAthletes = false,
            bool includeAthleteNavigation = false,
            bool includeSports = false)
        {
            var spec = Spec.Any<Anag_Master>();
            if (id.HasValue) spec &= AnagSpecifications.ByID(id.Value);
            if (!string.IsNullOrWhiteSpace(taxCode)) spec &= AnagSpecifications.ByTaxCode(search: taxCode, stringKind: searchStringKind);
            if (!string.IsNullOrWhiteSpace(vatNo)) spec &= AnagSpecifications.ByVATNo(search: vatNo, stringKind: searchStringKind);
            if (userId.HasValue) spec &= AnagSpecifications.ByUserID(userId.Value);
            if (athleteId.HasValue) spec &= AnagSpecifications.ByAthleteID(athleteId.Value);

            var query = _repository.GetBySpecification(spec);
            if (query.Count() != 1) return null;

            await _repository.Load_DirectNavigation(
                angIds: query.Select(t => t.Ang_ID).ToArray(),
                addresses: includeAddresses,
                contacts: includeContacts,
                documents: includeDocuments,
                athletes: includeAthletes,
                sports: includeSports);

            if (includeAthletes && includeAthleteNavigation)
                await _repository.Load_AthleteNavigation(athIds: query.Where(t => t.Athlete_Master != null).Select(t => t.Athlete_Master.Ath_ID).ToArray());

            return query.FirstOrDefault();
        }

        public async Task<IQueryable<Anag_Master>> ListAsync(
            AnagFilters filters,
            SearchStringKindEnum searchStringKind = SearchStringKindEnum.Contains,
            bool includeAddresses = false,
            bool includeContacts = false,
            bool includeDocuments = false,
            bool includeAthletes = false,
            bool includeAthleteNavigation = false,
            bool includeSports = false)
        {
            var spec = Spec.Any<Anag_Master>();
            if (filters?.Roles?.Count() > 0) spec &= AnagSpecifications.ByRoleIDs(filters.Roles.ToArray());
            if (filters?.CorporateRoles?.Count() > 0) spec &= AnagSpecifications.ByCorporateRoleIDs(filters.CorporateRoles.ToArray());
            if (!string.IsNullOrWhiteSpace(filters?.SearchString))
            {
                ASpec<Anag_Master> searchSpec = AnagSpecifications.ByCompleteName(filters.SearchString);
                searchSpec |= AnagSpecifications.ByTaxCode(filters.SearchString);
                searchSpec |= AnagSpecifications.ByVATNo(filters.SearchString);
                spec &= (searchSpec);
            }

            var query = _repository.GetBySpecification(spec);

            await _repository.Load_DirectNavigation(
                angIds: query.Select(t => t.Ang_ID).ToArray(),
                addresses: includeAddresses,
                contacts: includeContacts,
                documents: includeDocuments,
                athletes: includeAthletes,
                sports: includeSports);

            if (includeAthletes && includeAthleteNavigation)
                await _repository.Load_AthleteNavigation(athIds: query.Where(t => t.Athlete_Master != null).Select(t => t.Athlete_Master.Ath_ID).ToArray());

            return query;
        }

        public async Task<Anag_Master> SaveAsync(AnagDTO dto = null, Anag_Master entity = null)
        {
            if (dto == null && entity == null) throw new Exception($"{nameof(dto)} and {nameof(entity)} they were both null.");
            if (dto != null && entity == null)
            {
                if (dto.Ang_ID > 0) entity = await FindByAsync(id: dto.Ang_ID);
                entity ??= new Anag_Master();
                _mapper.Map(dto, entity);
            }

            if (dto?.Ang_ID <= 0 || entity.Ang_ID <= 0) entity = await _repository.AddAsync(entity);
            else entity = await _repository.UpdateAsync(entity);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return entity;
        }
    }
}
