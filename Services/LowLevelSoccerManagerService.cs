﻿using DAL;
using DAL.Model_Classes;
using System.Collections.Generic;

namespace Services
{
    public interface ILowLevelSoccerManagmentService
    {
        void CreatePlayerForTeam(int teamId, Player player);
        void RemovePlayer(int playerId);
        void UpdatePlayer(int playerId, Player updatedPlayer);
        IEnumerable<Player> GetAllPlayers();
    }

    public class LowLevelSoccerManagerService : ILowLevelSoccerManagmentService
    {
        private readonly IRepository<Player> _payerRepository;
        private readonly IRepository<Team> _teamRepository;

        public LowLevelSoccerManagerService(IRepository<Player> playerRepository, IRepository<Team> teamRepository)
        {
            _payerRepository = playerRepository;
            _teamRepository = teamRepository;
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

            _payerRepository.Update(player);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _payerRepository.GetAll();
        }
    }
}
