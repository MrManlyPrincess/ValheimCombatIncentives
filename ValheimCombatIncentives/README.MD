# Valheim Combat Incentives

## Features
Provides an experience incentive to perform certain actions within combat, and provides experience based on damage dealt/blocked.

## Installation (manual)
### Manual Install
Extract ValheimCombatIncentives.dll into the BepinEx/plugins folder.

## Config
### Default Config File

```
## Settings file was created by plugin Valheim Combat Incentives v0.0.1
## Plugin GUID: com.mrmanlyprincess.ValheimCombatIncentives

[Damage Multipliers]

## The multiplier to apply to damage dealt to determine the amount of bonus experience given.
# Setting type: Single
# Default value: 0.03
Dealt Damage Multiplier = 0.03

## The multiplier to apply to damage blocked to determine the amount of bonus experience given.
# Setting type: Single
# Default value: 0.1
Blocked Damage Multiplier = 0.1

## The multiplier to apply to surprise attack damage to determine the amount of sneak experience to be given.
# Setting type: Single
# Default value: 0.3
Sneak Damage Experience Multiplier = 0.3

[General]

## Enable this mod
# Setting type: Boolean
# Default value: true
Enabled = true

## Show notifications on XP events
# Setting type: Boolean
# Default value: true
Show Notifications = true

## The threshold after which to show a notification for an experience bonus.
# Setting type: Single
# Default value: 1
Notification Experience Threshold = 1

[Incentive Multipliers]

## The multiplier to apply AFTER the 'Dealt Damage Multiplier' when surprise attacks are performed 
# Setting type: Single
# Default value: 1.5
Knife Surprise Attack Multiplier = 1.5

## The multiplier to apply AFTER the 'Dealt Damage Multiplier' when secondary attacks are performed 
# Setting type: Single
# Default value: 1.1
Secondary Attack Multiplier = 1.1

## The multiplier to apply AFTER the 'Dealt Damage Multiplier' when bow attacks are performed.This is first applied to the distance that the ranged attack was made from, then the result is applied to the existing experienceBonus.
# Setting type: Single
# Default value: 0.1
Bow Distance Multiplier = 0.1

## The multiplier to apply AFTER the 'Dealt Damage Multiplier' when spear throws are performed.This is first applied to the distance that the ranged attack was made from, then the result is applied to the existing experienceBonus.
# Setting type: Single
# Default value: 0.2
Spear Distance Multiplier = 0.2

## The minimum multiplier to apply AFTER the 'Dealt Damage Multiplier' and the 'Bow Distance Multiplier' when bow attacks are performed.This is used (along with Bow Draw Percentage Multiplier - Maximum) to determine how to adjust the experience bonus based on how much the bow was drawn.
# Setting type: Single
# Default value: 0.4
Bow Draw Percentage Multiplier - Minimum = 0.4

## The maximum multiplier to apply AFTER the 'Dealt Damage Multiplier' and the 'Bow Distance Multiplier' when bow attacks are performed.This is used (along with Bow Draw Percentage Multiplier - Minimum) to determine how to adjust the experience bonus based on how much the bow was drawn.
# Setting type: Single
# Default value: 1.1
Bow Draw Percentage Multiplier - Maximum = 1.1

[Skill Multipliers]

## The multiplier to apply AFTER the 'Blocked Damage Multiplier', only in the case that a parry was achieved.
# Setting type: Single
# Default value: 1.2
Parry Bonus Experience Multiplier = 1.2

## The multiplier to apply AFTER the 'Sneak Damage Experience Multiplier', only in the case that the surprise attack killed the target.
# Setting type: Single
# Default value: 2
Assassination Sneak Experience Multiplier = 2
```

## Changelog
- **v0.0.1**
  - Initial version

## Known issues
- None
