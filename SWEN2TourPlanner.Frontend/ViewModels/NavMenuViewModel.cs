using SWEN2TourPlanner.Frontend.ViewModels.Interfaces;

namespace SWEN2TourPlanner.Frontend.ViewModels;

public class NavMenuViewModel : INavMenuViewModel {
    public String? GetCurrentTour() {
        return "CurrentTour";
    }
    public String? GetCurrentTourLog() {
        return null;
    }

    public String GetNavIcon() {
        bool isMenuExpanded = true;
        if (isMenuExpanded) {
            return "∧";
        } else {
            return "∨";
        }
    }

}
