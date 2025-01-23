using HammerTime.data;
using HammerTime.services;
using Microsoft.Extensions.DependencyInjection;

namespace HammerTime.Test;

[TestClass]
public class CombatTest
{
    [TestMethod]
    public void TestCombatClass()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddSingleton<CombatService>()
            .AddSingleton<IProjectService, ProjectService>()
            .AddSingleton<IDiceService, DiceService>()
            .BuildServiceProvider();

        var combatService = serviceProvider.GetService<CombatService>() ?? throw new InvalidOperationException("CombatService is not available.");
        var projectService = serviceProvider.GetService<IProjectService>() ?? throw new InvalidOperationException("ProjectService is not available.");

        int attackerId = projectService.AddSoldier(new BaseSoldierClass
        {
            attacks = 10,
            ballisticSkill = 1,
        });

        int defenderId = projectService.AddSoldier(new BaseSoldierClass
        {
            attacks = 10,
            ballisticSkill = 1,
        });

        combatService.CombatMove(attackerId, defenderId, 0, false, false);

        // Assert
    }
}