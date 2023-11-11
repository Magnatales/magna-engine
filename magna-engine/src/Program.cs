using Core;
using HelloWorld;

var test = new TestGame(new GameSettings(){Title = "Tactics Game!", IconPath = "resources/icon.png"});
test.Run(new TestYSortingScene("TestQuadtree"));