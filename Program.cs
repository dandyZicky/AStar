// See https://aka.ms/new-console-template for more information
using AStar;

int[] startingPose = [10, 4];
int[] endPose = [0, 1];

var search = new Search([11, 4],
      [4, 4]);

search.GridSearch(grid: new int[,]
        {
            {1, 0, 1, 1, 1, 1, 0, 1, 1, 1},
            {1, 1, 1, 0, 1, 1, 1, 0, 1, 1},
            {1, 1, 1, 0, 1, 1, 0, 1, 0, 1},
            {0, 0, 1, 0, 1, 0, 0, 0, 0, 1},
            // {1, 1, 1, 0, 1, 1, 1, 0, 1, 0},
            // {1, 0, 1, 1, 1, 1, 0, 1, 0, 0},
            // {1, 0, 0, 0, 0, 1, 0, 0, 0, 1},
            // {1, 0, 1, 1, 1, 1, 0, 1, 1, 1},
            // {1, 1, 1, 0, 0, 0, 1, 0, 0, 1}
        });
