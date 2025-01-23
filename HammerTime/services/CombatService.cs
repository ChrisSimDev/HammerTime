using HammerTime.data;

namespace HammerTime.services
{
    public class CombatService(IDiceService diceService, IProjectService projectService)
    {
        private IDiceService _diceService = diceService;
        private IProjectService _projectService = projectService;

        public void CombatMove(int attackerId, int[] defenderIds, int distance, bool overwatch, bool isMelee)
        {
            BaseSoldierClass attacker = _projectService.GetSoldier(attackerId);
            BaseSoldierClass[] defenders = [];
            for (int i = 0; i < defenderIds.Length; i++)
            {
                defenders[i] = (_projectService.GetSoldier(defenderIds[i]));
            }

            int attackCount = isMelee
                ? attacker.attacks 
                : 1;

            (int hitCount, int critCount) = CalculateHits(attacker.attacks, attacker.ballisticSkill, false, 0, false, false);
            _projectService.SetSoldier(defenderId, defender);

        }

        private (int, int) CalculateHits(int numAttacks, int ballisticWeaponSkill, bool torrent = false, int combatModifier = 0, bool rerollOnes = false, bool overwatchStatus = false)
        {
            if (torrent)
            {
                // Torrent guarantees all hits
                return (numAttacks, 0);
            }

            // Dice Roll
            int[] rolls = _diceService.RollDice(numAttacks, 6);
            
            // Re-roll ones
            if (rerollOnes)
            {
                for (int i = 0; i < numAttacks; i++)
                {
                    if (rolls[i] == 1)
                    {
                        rolls[i] = _diceService.RollDice(1, 6)[0];
                    }
                }
            }

            // Hit Calculation
            int hits = 0;
            int crits = 0;
            int effectiveWeaponSkill = 6;

            if (!overwatchStatus)
            {
                effectiveWeaponSkill = ballisticWeaponSkill + combatModifier;
            }

            foreach (int roll in rolls)
            {
                if (roll == 6)
                {
                    crits++;
                    hits++;
                }
                else if (roll == 1)
                {
                    continue;
                }
                else if (roll >= effectiveWeaponSkill)
                {
                    hits++;
                }
            }
            return (hits, crits);
        }
        public static (BaseSoldierClass, int) ApplyWounds(BaseSoldierClass defenderSoldier, int hitCount, int critCount)
        {
            // Calculate wounds
            return (new BaseSoldierClass(), 0);
        }

        // public static double GetWounds(double nht, int wsr, int tug, int ams, int pic, int dmg, double crt = 0, int ivs = 7, bool twl = false, int mod = 0, bool cov = false, int ant = 6, int fnp = 7, bool dev = false, bool let = false)
        // {
        //     double wratio = (double)wsr / tug;
        //     int basewthres
        //     if (wratio == 1)
        //     {
        //         basewthreshold = 4;
        //     }
        //     else if (wratio < 1 && wratio > 0.5)
        //     {
        //         basewthreshold = 5;
        //     }
        //     else if (wratio <= 0.5)
        //     {
        //         basewthreshold = 6;
        //     }
        //     else if (wratio > 1 && wratio < 2)
        //     {
        //         basewthreshold = 3;
        //     }
        //     else
        //     {
        //         basewthreshold = 2;

        //     int effwthreshold = Math.Max(Math.Min(basewthreshold - mod, ant), 2);
        //     double ehit = nht - (let ? crt : 0);
        //     double basefailw = ehit * (effwthreshold - 1) / 6.0;
        //     double crits = (ehit + (twl ? basefailw : 0)) * (7 - ant) / 6.0;
        //     double wounds = (ehit + (twl ? basefailw : 0)) * Math.Max((ant - 2 - (effwthreshold - 2)) / 6.0, 0) + (let ? crt : 0);
        //     double totalwounds = crits + wo
        //     Console.WriteLine($"{Math.Round(totalwounds, 3)} successful wounds, of which {Math.Round(crits, 3)} critical woun
        //     if (pic == 0 && ams <= 3)
        //     {
        //         cov = false;

        //     int effamr = Math.Min(ams + pic - (cov ? 1 : 0), ivs);
        //     double saveprob = Math.Max((7 - effamr) / 6.0, 0);
        //     double potwounds = (dev ? crits : 0) + (wounds + (dev ? 0 : crits)) * (1 - saveprob);
        //     double truewounds = potwounds * (1 - Math.Max((7 - fnp) / 6.0, 0)) *
        //     Console.WriteLine($"average number of wounds is {Math.Round(truewounds, 3
        //     return truewounds;
    }
}

//natk-number of attacks-int
//bws-ballistic/weapon skill-int
//trt-torrent-bool
//mod-modifier-int
//rr0-reroll ones-bool
//ovw-overwatch-bool
//crt-get crits-bool

//torrent bypasses hitcheck

//crits=natural 6s

//minimum possible weapon skill is 2

//overwatch forces weapon skill=6

//hitprob:probability of successful hit for a single attack

//nht:number of hits-int
//crt: number of critical hits-int
//wsr:weapon strength-int
//tug: target toughness-int
//ams: armour save-int
//ivs: invulnerable save-int
//pic: armour piercing-int
//dmg: damage-int
//twl: twin-linked-bool
//mod: wound modifier-int
//cov: cover-bool
//ant: anti-int
//fnp: feel no pain-int
//dev: devastating wounds-bool
//let: lethal hits-bool
//basewthreshold: base wound threshold
//effwthreshold: effective wound threshold-roll needed to score a potential wound
//effective wound threshold cannot be less than 2 or greater than 6
//ehit: effective hits where wound roll is calculated
//basefailw: probable number of failures to wound before twin-linked
//crits: expected number of critical wounds
//wounds: expected number of normal wounds
//ignore cover if armour save is better than 3 and piercing is 0
//effamr: effective armour save-cannot be worse than invulnerable save
//saveprob: probability of successful armour save
//potwounds: expected number of wounding attacks