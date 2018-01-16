# Lean-To-Move Demo
![Lean-To-Move](https://raw.githubusercontent.com/Erupac/LeanToMove/master/Images/ScreenShot3.PNG)
This repository contains a unity demo of the Lean-To-Move locomotion mechanic. The core scripts can be found in `Assets/Scripts/`. For developers, `PivotMove.cs` provides an abstract class that handles all the core mechanics of this kind of locomotion. `LinearPivotToMove.cs` shows a very simple implementation of `PivotMove` and is the implementation used in the demo.

# Running
Download the repository and run LeanToMoveDemo.exe.

# Introduction
The purpose of this document is to explain some fundamental properties of the human balance system, to propose some guidelines on how to create a comfortable locomotion mechanic, and to demonstrate a proof-of-concept locomotion mechanic that aligns with this understanding.
# Background 
The human body relies on three types of sensory input to maintain balance: the vestibular system, the visual system, and the somatosensory system [1, 3].
## The Vestibular System
The vestibular system is made up of the fluid filled tubes and hair lined sacs in the inner ear that detect rotational and linear acceleration. With this system, the body can tell which way is up in relation to gravity and can feel rotation.
## The Visual System
The visual system gets its input from the eyes. By visually tracking the surroundings, it’s possible to gain some information about the orientation and movement of the body.
## The Somatosensory System
The somatosensory system encompasses proprioception, which is the body’s ability to sense its own configuration in space, and exteroception, which is all the pressure and strain sensations that come from the environment.


As information streams in from these three systems, the brain synthesizes what it believes to be the reality of the body’s movement and orientation. In ordinary circumstances, the three streams of information corroborate each other and balance is easily maintained. However, when the information from any of these three systems conflicts, the brain has to do some reconciliation in order to decide what’s really going on. This effect can often be unpleasant and is one cause of motion sickness and vertigo.


Take, for instance, the case where one becomes dizzy by spinning themselves around quickly and then stopping. The sudden stop causes the fluids in the vestibular system to continue flowing for some time after the stop. This creates a conflict of information, since the vestibular system is still reporting a rotational acceleration while the visual and somatosensory systems both report a stationary reality. As many have experienced, the brain takes the vestibular system as truth and thus a person feels the world spinning and sees the horizon rotating as the brain reconciles the conflict.


The reconciliation process that happens in the event of a conflict is a function of the intensity of the stimulus as well as some weighted preferences. The brain trusts information from some systems more than others, but if the stimuli from an untrusted system is intense enough, it may end up trusting that system nevertheless. The exact weights are not precisely known and, in fact, likely differ from person to person, but generally speaking, the vestibular system has a higher weight than the visual and somatosensory systems [2].
# Application To VR
If the brain is sufficiently convinced of a particular reality, then one can avoid motion sickness. Even though the exact reconciliation function is unknown (and could even vary between individuals) a good rule of thumb for use in VR is that if you can convincingly simulate 2 of the 3 balance systems, motion sickness can generally be avoided.


Because it can be difficult to simulate the vestibular system in VR, simulation is limited to primarily the somatosensory and visual systems. The locomotion mechanisms that have proven to work well leverage the 2 out of 3 rule. For instance, the “walking-arm” locomotion, used in games such as The Art Of Fight, recreates the body forces that one associates with walking (the somatosensory system) and pairs it with what is happening in the headset (the visual system).


With the introduction of  walking peripherals like the Virtuix Omni, the somatosensory system can be highly stimulated in order to convince the brain that 1. what it is seeing in the headset is reality, and 2. the vestibular system should be ignored. Interestingly, although it is advantageous to stimulate the somatosensory system as much as possible, it is possible to get away with less than a perfect recreation of locomotion. This is why locomotion methods like “walking-arms” are generally successful. 
# Lean To Move
Based on this understanding, the “Lean to Move” locomotion mechanism was developed. Lean-to-Move is a fairly basic result from these rules, taking only the most obvious solution when considering ways to stimulate the somatosensory system. The idea is that by taking a step in the direction you want to go, you give your brain enough conviction of movement that when the in-game avatar also moves, the brain believes it.


The mechanics of Lean-to-Move are simple. The player initiates the movement by pressing the trigger. The system then stores the location of the head when the trigger was pressed as the origin of movement.

Next, the system waits for the player to lean or move outside of the threshold region. Once the player breaches this radius, the distance between the head and the wall of the threshold is used to compute the player’s velocity.


[Reference Pictures](http://imgur.com/a/m8ioB)
# Advantages of Lean-to-Move
Lean-to-Move helps to mitigate nausea in a similar way to other locomotion strategies being deployed today. One major advantage of Lean-to-Move is that it frees up the player’s hands to be able to do whatever the game requires. In the FPS setting, for instance, a player can more naturally wield their weapon while moving around. Lean-to-Move also has a low activity overhead, meaning that players can use it for longer without experiencing fatigue.
# Current Issues 
The best results seem to be obtained when the player keeps one foot stationary while stepping. There is, however, no way to enforce this and even with reminders to the player, it’s easy to forget to do this. Nausea can occur if the footing is not maintained.


This locomotion style also seems to encourage drift from the center of the play space. Eventually the player ends up near the edge of the playspace where they can no longer take a step. This causes a break in immersion as well as frustration.


The simple implementation described above also doesn’t lend itself to easy turning. In order to change direction, the player must return to the origin, reorient their body, and then step in the correct direction. This was the primary cause of players losing their anchored foot. 
Possible Improvements
To address some of the issues above, a number of improvements can be made to the implementation described above. To address the cumbersome change in direction, it may be possible to incorporate the change in rotation of the head into the direction of travel.


To address the variability of the tolerances to nausea, it may be possible to allow players to tune the locomotion parameters to something that feels the most correct. This includes the threshold distance, the max speed, and the gain applied to the lean displacement when calculating the velocity.
# Conclusion
Lean-To-Move exhibits promising mitigation of nausea while enabling more natural movement in VR games. While Lean-To-Move is not perfect, it does exploit the general mechanics of nausea and proposes a possible locomotion strategy that can be iterated upon to achieve traditional first person locomotion. In addition, by following some of the guidelines presented above, it is possible to come up with even more locomotion strategies that could give people an even better experience in VR. 
# References
* [1] Balance Training Bootcamp. (n.d.). Retrieved November 12, 2016, from https://www.vewdo.com/Balance-Training-Bootcamp_ep_62-1.html
* [2] Butler, J. S., Smith, S. T., Campos, J. L., & Bulthoff, H. H. (2010, 09). Bayesian integration of visual and vestibular signals for heading. Journal of Vision, 10(11), 23-23. doi:10.1167/10.11.23
* [3] How does our sense of balance work? - National Library of Medicine - PubMed Health. (2016, August 12). Retrieved November 12, 2016, from https://www.ncbi.nlm.nih.gov/pubmedhealth/PMH0072578/
