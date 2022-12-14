using Sandbox.Internal;

namespace Frightworks;

// Handles the ongoing state
public partial class MatchBehavior
{
	bool startedSpawningHelicopter = false;
	bool helicopterSpawned = false;
	TimeUntil untilHelicopterSpawns;

	bool areAllGeneratorsPowered = false;

	bool CanBecomeOngoing()
	{
		foreach ( (var steamId, var _) in PlayerRoles )
		{
			var exists = Game.Clients.Any( c => c.SteamId == steamId );
			if ( !exists ) return false;
		}

		return true;
	}

	void BecomeOngoing()
	{
		State = MatchState.Ongoing;

		SpawnPlayers();
		SpawnObjectives();
	}

	void TickOngoingState()
	{
		if ( !areAllGeneratorsPowered )
		{
			var powered = Entity.All.OfType<Generator>()
				.Where( g => g.IsPowered );

			areAllGeneratorsPowered = powered.Count() >= GameSettings.PoweredGeneratorsNeeded;
			if ( !areAllGeneratorsPowered ) return;
		}

		if ( !startedSpawningHelicopter )
		{
			startedSpawningHelicopter = true;
			untilHelicopterSpawns = GameSettings.HelicopterArriveTime;
			return;
		}

		if ( untilHelicopterSpawns > 0f ) return;

		if ( helicopterSpawned ) return;
		helicopterSpawned = true;

		SpawnHelicopter();
	}

	void SpawnPlayers()
	{
		var survivorSpawns = Entity.All.OfType<SurvivorSpawn>().ToList();
		var slasherSpawns = Entity.All.OfType<SlasherSpawn>().ToList();

		var survivorSpawnId = 0;
		var slasherSpawnId = 0;

		foreach ( var (steamId, readyAs) in PlayerRoles )
		{
			var client = Game.Clients.First( c => c.SteamId == steamId );

			BasePlayer plr = readyAs switch
			{
				ReadyAs.Slasher => CreateSlasherPlayer(),
				ReadyAs.Survivor or _ => new SurvivorPlayer(),
			};
			client.Pawn = plr;

			if ( plr is SurvivorPlayer surv )
			{
				var spawn = survivorSpawns[survivorSpawnId];
				surv.Transform = spawn.Transform;
				surv.ResetInterpolation();

				surv.Respawn();

				survivorSpawnId = (survivorSpawnId + 1) % survivorSpawns.Count;
			}

			if ( plr is SlasherPlayer slasher )
			{
				var spawn = slasherSpawns[slasherSpawnId];
				slasher.Transform = spawn.Transform;
				slasher.ResetInterpolation();

				slasher.Respawn();

				slasherSpawnId = (slasherSpawnId + 1) % slasherSpawns.Count;
			}
		}
	}

	void SpawnObjectives()
	{
		// Generators and batteries
		var generatorSpawns = Entity.All.OfType<GeneratorSpawn>()
			.OrderBy( _ => Guid.NewGuid() )
			.ToList();

		int genCount = 0;
		foreach ( var generatorSpawn in generatorSpawns )
		{
			if ( genCount == GameSettings.GeneratorCount ) break;

			var batterySpawns = Entity.All.OfType<CarBatterySpawn>()
				.Where( b => b.GeneratorId == generatorSpawn.GeneratorId )
				.ToList();

			Game.SetRandomSeed( Time.Tick + (genCount * 5) );
			var batterySpawn = Game.Random.FromList( batterySpawns );

			_ = new Generator
			{
				Transform = generatorSpawn.Transform,
			};
			_ = new CarBattery
			{
				Transform = batterySpawn.Transform,
			};

			genCount++;
		}

		// Gas cans
		var gasCanSpawns = Entity.All.OfType<GasCanSpawn>()
			.OrderBy( _ => Guid.NewGuid() )
			.ToList();

		int gasCanGoal = genCount * GameSettings.GasCansPerGenerator;
		int gasCanCount = 0;
		foreach ( var fuelCanSpawn in gasCanSpawns )
		{
			if ( gasCanCount == gasCanGoal ) break;

			_ = new GasCan
			{
				Transform = gasCanSpawns[gasCanCount % gasCanSpawns.Count].Transform,
			};

			gasCanCount++;
		}
	}

	void SpawnHelicopter()
	{
		var helicopterSpawn = Entity.All.OfType<HelicopterSpawn>()
			.OrderBy( _ => Guid.NewGuid() )
			.First();

		var heli = new Helicopter();

		heli.Transform = helicopterSpawn.Transform;
	}
}
