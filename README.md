
# State Machine Pattern

State Machine Pattern in Unity which depicts a simple AI system with three distinct states: "Follow Player," "Attack on Enemy," and "Dead." The AI should transitions these states based on specific conditions.

## State Machine
Unity project that demonstrates an AI agent (represented by a sphere) that can perform the following behaviors:

Follow Player:

    o The AI follows the player character when within a certain distance.

    o Implemented basic movement logic for the AI to move towards the player.

Attack on Enemy:

    o If an enemy comes within a certain range of the AI, it switches from following the player to attacking the enemy.

    o The attack is represented by moving towards the enemy.

    o Implemented a simple mechanism to switch from following the player to attacking the enemy based on distance.

Dead:

    o The AI should enter the "Dead" state when it has zero health.

    o Once in the "Dead" state, the AI should stop all movement and actions.




