using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;


namespace FullstackOpdracht.Repositories
{
    public class ExtendedMatchDAO : MatchDAO
    {
        private readonly VoetbalDbContext _dbContext;

        public ExtendedMatchDAO(VoetbalDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Match>> GetMatchesForTeam(int teamId)
        {
            try
            {
                return await _dbContext.Matches
                                   .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                                   .Where(m => m.MatchDate > DateTime.Now)
                                   .Include(m => m.HomeTeam)
                                   .Include(m => m.AwayTeam)
                                   .ToListAsync();
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Match>> GetTicketMatches()
        {
            try
            {
                return await _dbContext.Matches
                                    .Where(m => m.MatchDate < DateTime.Now.AddMonths(1))
                                    .Where(m => m.MatchDate > DateTime.Now)
                                    .Include(m => m.HomeTeam)
                                    .Include(m => m.AwayTeam)
                                    .ToListAsync();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<IEnumerable<Match>> GetMatchesBetweenTeams(int idHomeTeam, int idVisitingTeam)
        {
            try
            {
                return await _dbContext.Matches
                                    .Where(m => m.AwayTeamId == idHomeTeam || m.AwayTeamId == idVisitingTeam)
                                    .Where(m => m.HomeTeamId == idHomeTeam || m.HomeTeamId == idVisitingTeam)
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

    }
}
