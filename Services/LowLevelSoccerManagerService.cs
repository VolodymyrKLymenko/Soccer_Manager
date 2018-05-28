using DAL;
using DAL.Model_Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface ILowLevelSoccerManagmentService
    {
        void CreatePlayerForTeam(int teamId, Player player);
        void RemovePlayer(int playerId);
        void AddRewardForTeam(int teamId, Reward reward);
        void UpdateReward(int rewardId, Reward updatedReward);
        IEnumerable<Reward> GetTeamRewards(int teamId);
        IEnumerable<Reward> GetAllRewards();
        Reward GetReward(int rewardId);
        void RemoveReward(int rewardId);
        Player GetPlayer(int playerId);
        void UpdatePlayer(int playerId, Player updatedPlayer);
        IEnumerable<Player> GetAllPlayers();
    }

    public class LowLevelSoccerManagerService : ILowLevelSoccerManagmentService
    {
        private readonly IRepository<Player> _payerRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Reward> _rewardRepository;

        public LowLevelSoccerManagerService(IRepository<Player> playerRepository, IRepository<Team> teamRepository, 
            IRepository<Reward> rewardRepository)
        {
            _payerRepository = playerRepository;
            _teamRepository = teamRepository;
            _rewardRepository = rewardRepository;
        }

        public void CreatePlayerForTeam(int teamId, Player player)
        {
            var team = _teamRepository.Get(t => t.TeamId == teamId);
            if (team.Players == null)
            {
                team.Players = new List<Player>();
            }
            team.Players.Add(player);
            _teamRepository.Update(team);
        }

        public void AddRewardForTeam(int teamId, Reward reward)
        {
            var team = _teamRepository.Get(t => t.TeamId == teamId);
            if (team.Rewards == null)
            {
                team.Rewards = new List<Reward>();
            }
            team.Rewards.Add(reward);
            _teamRepository.Update(team);
        }

        public IEnumerable<Reward> GetTeamRewards(int teamId)
        {
            return _rewardRepository.GetAll().Where(r => r.TeamId == teamId);
        }

        public IEnumerable<Reward> GetAllRewards()
        {
            return _rewardRepository.GetAll();
        }

        public void RemoveReward(int rewardId)
        {
            var reward = _rewardRepository.Get(r => r.RewardId == rewardId);

            _rewardRepository.Delete(reward);
        }

        public Reward GetReward(int rewardId)
        {
            return _rewardRepository.Get(rewardId);
        }

        public void UpdateReward(int rewardId, Reward updatedReward)
        {
            var reward = _rewardRepository.Get(r => r.RewardId == rewardId);

            reward.Name = updatedReward.Name;
            reward.Date = updatedReward.Date;

            _rewardRepository.Update(reward);
        }

        public Player GetPlayer(int playerId)
        {
            var res =_payerRepository.Get(playerId);
            res.Born = res.Born;
            return res;
        }

        public void RemovePlayer(int playerId)
        {
            var player = _payerRepository.Get(p => p.PlayerId == playerId);

            _payerRepository.Delete(player);
        }

        public void UpdatePlayer(int playerId, Player updatedPlayer)
        {
            var player = _payerRepository.Get(p => p.PlayerId == playerId);

            player.Name = updatedPlayer.Name;
            player.Position = updatedPlayer.Position;
            player.Surname = updatedPlayer.Surname;
            player.Born = updatedPlayer.Born;

            _payerRepository.Update(player);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            var res = _payerRepository.GetAll();
            foreach (var item in res)
            {
                item.Age_ = Age(item.Born);
            }

            return res;
        }


        public static void RecalculateAge(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.Age_ = Age(player.Born);
            }
        }

        private static int Age(DateTime date)
        {
            return DateTime.Now.Year - date.Year;
        }
    }
}