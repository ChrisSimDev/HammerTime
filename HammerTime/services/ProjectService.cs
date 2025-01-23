using HammerTime.data;

namespace HammerTime.services
{
    public class ProjectService : IProjectService
    {
        private Project _project = new(){
            Name = "HammerTime",
        };

        public int AddSoldier(BaseSoldierClass soldier)
        {
            _project.CurrSoldierId++;
            _project.SoldierDictionary.Add(_project.CurrSoldierId, soldier);
            return _project.CurrSoldierId;
        }

        public void RemoveSoldier(int soldierId)
        {
            _project.SoldierDictionary.Remove(soldierId);
        }

        public void SetSoldier(int soldierId, BaseSoldierClass soldier)
        {
            _project.SoldierDictionary[soldierId] = soldier;
        }
        
        public BaseSoldierClass GetSoldier(int soldierId)
        {
            return _project.SoldierDictionary[soldierId];
        }

    }
}