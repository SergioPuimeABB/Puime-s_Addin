using ABB.Robotics.RobotStudio.Stations;
using ABB.Robotics.RobotStudio.Environment;

namespace Puime_s_Addin
{
    public class Hide_CS
    {
        public static void Hide_Objects()
        {
            // Will hide the frames (targets) and paths using the RobotStudio CommandBarButton

            // Frames (targets, workobjects, frames)
            bool FramesVisible = CommandBarButton.FromID("ViewShowAllFrames").IsChecked; // checks if the Command is checked

            if (FramesVisible) // if the option is active desactive it
            {
                CommandBarButton.FromID("ViewShowAllFrames").Execute();
            }
            else // if the option is desactivated, active and desactive it. If some frame is creted with the option off, the frame is still visible
            {
                CommandBarButton.FromID("ViewShowAllFrames").Execute();
                CommandBarButton.FromID("ViewShowAllFrames").Execute();
            }

            // Paths
            bool PathsVisible = CommandBarButton.FromID("ViewShowAllPaths").IsChecked; // checks if the Command is checked

            if (PathsVisible)
            {
                CommandBarButton.FromID("ViewShowAllPaths").Execute();
            }
            else
            {
                CommandBarButton.FromID("ViewShowAllPaths").Execute();
                CommandBarButton.FromID("ViewShowAllPaths").Execute();
            }
        }

        public static void ResetFloor()
        {
            // Will Reset the floor size using the RobotStudio CommandBarButton
            CommandBarButton.FromID("ViewResetFloorSize").Execute();

            // Hide the RobotStudio floor
            bool FloorVisible = CommandBarButton.FromID("ViewShowFloor").IsChecked; // check if the Command ShowFloor is checked
            
            if (FloorVisible)
            {
                CommandBarButton.FromID("ViewShowFloor").Execute();
            }
        }
    }
}
