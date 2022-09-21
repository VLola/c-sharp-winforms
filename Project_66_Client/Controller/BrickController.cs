using Project_66_Client.Model;
using Project_66_Client.View;

namespace Project_66_Client.Controller
{
    internal class BrickController
    {
        BrickView _brickView;
        BrickModel _brickModel;
        public BrickController(BrickView brickView, BrickModel brickModel)
        {
            _brickView = brickView;
            _brickModel = brickModel;
            Load();
        }
        public void Load()
        {
            _brickView.Relocation(_brickModel.X, _brickModel.Y);
        }
        public BrickView GetBrickView()
        {
            return _brickView;
        }
        public BrickModel GetBrickModel()
        {
            return _brickModel;
        }
    }
}
