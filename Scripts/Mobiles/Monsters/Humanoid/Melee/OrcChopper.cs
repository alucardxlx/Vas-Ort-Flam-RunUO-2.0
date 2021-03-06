using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an orcish corpse" )]
	public class OrcChopper : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.WhirlwindAttack;
		}

		public override InhumanSpeech SpeechType { get { return InhumanSpeech.Orc; } }

		[Constructable]
		public OrcChopper()
			: base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an orc chopper";
			Body = 138;
			BaseSoundID = 0x45A;
			Hue = 2213;

			SetStr( 147, 245 );
			SetDex( 91, 115 );
			SetInt( 61, 85 );
			SetHits( 97, 139 );
			SetDamage( 32, 37 );

			SetSkill( SkillName.MagicResist, 90.1, 95.0 );
			SetSkill( SkillName.Tactics, 75.1, 90.0 );
			SetSkill( SkillName.Wrestling, 60.1, 85.0 );

			SetFameLevel( 2 );
			SetKarmaLevel( 2 );

			VirtualArmor = 15;

			Item ExecutionersAxe = new ExecutionersAxe();
			AddItem( ExecutionersAxe );

			PackItem( new LambLeg( 3 ) );

			switch ( Utility.Random( 2 ) )
			{
				case 0: PackItem( new Log( 10 ) ); break;
				case 1: PackItem( new Board( 25 ) ); break;
			}

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses { get { return true; } }
		public override int Meat { get { return 1; } }

		public override OppositionGroup OppositionGroup
		{
			get { return OppositionGroup.SavagesAndOrcs; }
		}

		public override bool IsEnemy( Mobile m )
		{
			if ( m.Player && m.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
				return false;

			return base.IsEnemy( m );
		}

		public override void AggressiveAction( Mobile aggressor, bool criminal )
		{
			base.AggressiveAction( aggressor, criminal );

			Item item = aggressor.FindItemOnLayer( Layer.Helm );

			if ( item is OrcishKinMask )
			{
				AOS.Damage( aggressor, 50, 0, 100, 0, 0, 0 );
				item.Delete();
				aggressor.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				aggressor.PlaySound( 0x307 );
			}
		}

		public OrcChopper( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}