# BadgesSystem

A simple example of Achievements/Badges system.
Made with Windows 8.1 WinRT app in mind, but with a few small edits can be ported to be compatible with any platform.

#Usage

Define your badges in the `BadgeManager.cs` file inside the static badges list like so:

	new Badge("Name", "Description.", async () => { return Condition(); }, points),
	
The `condition` can be set to `null` if you wish to unlock the badge manually. More info below.

You can unlock badges either via the static method `CheckForUnlockedBadges()`, or by calling the badge's `Unlock()` method directly.

Badges with condition set to `null` can only be unlocked directly.