using HammerTime.data;

namespace HammerTime.services
{
    public interface IProjectService
    {
        public int AddSoldier(BaseSoldierClass soldier);
        public void SetSoldier(int soldierId, BaseSoldierClass soldier);
        public void RemoveSoldier(int soldierId);
        public BaseSoldierClass GetSoldier(int soldierId);
    }
}