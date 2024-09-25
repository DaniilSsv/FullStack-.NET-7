using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;

namespace FullstackOpdracht.Services
{
    public class ExtendedMatchService : MatchService
    {
        private ExtendedMatchDAO _matchDAO;

        public ExtendedMatchService(ExtendedMatchDAO matchDAO) : base(matchDAO)
        {
            _matchDAO = matchDAO;
        }

        public async Task<IEnumerable<Match>> GetMatchesForTeam(int teamId)
        {
            return await _matchDAO.GetMatchesForTeam(teamId);
        }

        public async Task<IEnumerable<Match>> GetTicketMatches()
        {
            return await _matchDAO.GetTicketMatches();
        }

        public async Task<IEnumerable<Match>> GetMatchesBetweenTeams(int idHomeTeam, int idVisitingTeam)
        {
            return await _matchDAO.GetMatchesBetweenTeams(idHomeTeam, idVisitingTeam);
        }

    }
}
