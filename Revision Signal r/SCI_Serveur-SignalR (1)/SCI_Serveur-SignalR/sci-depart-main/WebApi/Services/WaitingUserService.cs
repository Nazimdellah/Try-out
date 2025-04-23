namespace Super_Cartes_Infinies.Services
{
    public class UsersReadyForAMatch
    {
        public UsersReadyForAMatch(string userAId, string userBId, string userAConnectionId)
        {
            UserAId = userAId;
            UserBId = userBId;
            UserAConnectionId = userAConnectionId;
        }

        public string UserAId { get; set; }
        public string UserBId { get; set; }
        public string UserAConnectionId { get; set; }
    }

	public class WaitingUserService
    {
        private string? _userAId = null;
        private string? _userAConnectionId = null;
        private SemaphoreSlim _semaphore;

        public string UserAId { get { return _userAId; } }

        public WaitingUserService()
        {
            _semaphore = new SemaphoreSlim(1);
        }

        public async Task<bool> StopWaitingUser(string userId)
        {
            await _semaphore.WaitAsync();
            bool stoppedWaiting = false;
            if (_userAConnectionId == userId)
            {
                _userAConnectionId = null;
                _userAConnectionId = null;
                stoppedWaiting = true;
            }
            _semaphore.Release();
            return stoppedWaiting;
        }

        // Retourne null si il n'y a pas déjà un user qui attend pour jouer
        // Si non, on retourne la paire de Users
        public async Task<UsersReadyForAMatch?> LookForWaitingUser(string userId, string? connectionId)
        {
            // Si c'est encore le même player qui attendait déjà, on retourne null
            if (_userAId == userId)
                return null;

            await _semaphore.WaitAsync();

            // Aucun match en attente
            if (_userAId == null)
            {
                _userAId = userId;
                _userAConnectionId = connectionId;
                _semaphore.Release();
                return null;
            }
            else
            {
                var matchCreationResult = new UsersReadyForAMatch(_userAId, userId, _userAConnectionId!);
                _userAId = null;
                _userAConnectionId = null;
                _semaphore.Release();
                return matchCreationResult;
            }
        }

        
    }
}

