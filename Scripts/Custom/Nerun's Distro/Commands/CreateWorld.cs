// By Nerun
// CreateWorld gump
using System;
using System.Collections;
using System.IO;
using Server;
using Server.Mobiles; 
using Server.Items;
using Server.Scripts.Commands;
using Server.Network;
using Server.Commands;
using Server.Gumps;

namespace Server.Scripts.Commands 
{
	public class CreateWorld
	{
		public CreateWorld()
		{
		}

        public static void Initialize() 
        { 
            CommandSystem.Register( "Createworld", AccessLevel.Administrator, new CommandEventHandler( Create_OnCommand ) ); 
        } 

        [Usage( "just use [createworld" )]
        [Description( "Create world with a menu." )]
        private static void Create_OnCommand( CommandEventArgs e )
        {
                e.Mobile.SendGump( new CreateWorldGump( e ) );
	}
}
}
namespace Server.Gumps
{

    public class CreateWorldGump : Gump
    {
        private CommandEventArgs m_CommandEventArgs;
        public CreateWorldGump( CommandEventArgs e ) : base( 50,50 )
        {
            m_CommandEventArgs = e;
            Closable = true;
            Dragable = true;

            AddPage(1);

	//fundo cinza
	//x, y, largura, altura, item
            AddBackground( 0, 0, 200, 245, 5054 );
	//----------
            AddLabel( 30, 2, 200, "CREATE WORLD GUMP" );
	//fundo branco
	//x, y, largura, altura, item
            AddImageTiled( 10, 20, 180, 175, 3004 );
	//----------
            AddLabel( 20, 26, 200, "Decorate" );
            AddLabel( 20, 51, 200, "DoorGen" );
            AddLabel( 20, 76, 200, "MoonGen" );
            AddLabel( 20, 101, 200, "SignGen" );
            AddLabel( 20, 126, 200, "TelGen" );
            AddLabel( 20, 151, 200, "GenGauntlet" );
            AddLabel( 20, 176, 200, "GenChampions" );
	//Options
            AddCheck( 160, 23, 210, 211, true, 101 );
            AddCheck( 160, 48, 210, 211, true, 102 );
            AddCheck( 160, 73, 210, 211, true, 103 );
            AddCheck( 160, 98, 210, 211, true, 104 );
	    AddCheck( 160, 123, 210, 211, true, 105 );
            AddCheck( 160, 148, 210, 211, true, 106 );
	    AddCheck( 160, 173, 210, 211, true, 107 );

	//Ok, Cancel
            AddButton( 30, 205, 247, 249, 1, GumpButtonType.Reply, 0 );
            AddButton( 100, 205, 241, 243, 0, GumpButtonType.Reply, 0 );

        }

        public override void OnResponse( NetState state, RelayInfo info ) 
        { 
            Mobile from = state.Mobile; 

            switch( info.ButtonID ) 
            { 
                case 0: // Closed or Cancel
                {
                    return;
                }

                default: 
                { 
                    // Make sure that the OK, button was pressed
                    if( info.ButtonID == 1 )
                    {
                        // Get the array of switches selected
                        ArrayList Selections = new ArrayList( info.Switches );
			string prefix = CommandSystem.Prefix;

			from.Say( "CREATING WORLD..." );

                        // Now use any selected command
                        if( Selections.Contains( 101 ) == true )
                        {
                            from.Say( "Decorating world..." );
                            CommandSystem.Handle( from, String.Format( "{0}decorate", prefix ) );
                        }

                        if( Selections.Contains( 102 ) == true )
                        {
                            from.Say( "Generating doors..." );
                            CommandSystem.Handle( from, String.Format( "{0}doorgen", prefix ) );
                        }

                        if( Selections.Contains( 103 ) == true )
                        {
                            from.Say( "Generating moongates..." );
                            CommandSystem.Handle( from, String.Format( "{0}moongen", prefix ) );
                        }

                        if( Selections.Contains( 104 ) == true )
                        {
                            from.Say( "Generating signs..." );
                            CommandSystem.Handle( from, String.Format( "{0}signgen", prefix ) );
                        }

                        if( Selections.Contains( 105 ) == true )
                        {
                            from.Say( "Generating teleporters..." );
                            CommandSystem.Handle( from, String.Format( "{0}telgen", prefix ) );
                        }

                        if( Selections.Contains( 106 ) == true )
                        {
                            from.Say( "Generating Gauntlet spawners..." );
                            CommandSystem.Handle( from, String.Format( "{0}gengauntlet", prefix ) );
                        }

                        if( Selections.Contains( 107 ) == true )
                        {
				// champions message in champions script
                            CommandSystem.Handle( from, String.Format( "{0}genchampions", prefix ) );
                        }

                    }

                    from.Say( "World generation completed!" );

                    break;
                } 
            } 
        }
    }
}