using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class ReachesOper
    {
        public ReachesOper()
        {

        }
        public IList<Group> GroupReaches(ref IList<Reach> reaches)
        {
            IList<Group> groups = new List<Group>();
            for (int i = 0; i < reaches.Count; i++)
            {
                CalInDegree(ref reaches, i);
                if (reaches[i].InDeg == 2)
                {
                    Group group = new Group();
                    group.main = reaches[i].ID;
                    group.node = reaches[i].points[0];
                    for (int j = 0; j < reaches.Count; j++)
                    {
                        if (reaches[i].points[0].GetX() == reaches[j].points[reaches[j].points.Count - 1].GetX()
                            && reaches[i].points[0].GetY() == reaches[j].points[reaches[j].points.Count - 1].GetY())
                        {
                            if (group.tributary1 == -1)
                            {
                                group.tributary1 = reaches[j].ID;
                                reaches[i].upstream1 = reaches[j].ID;
                                reaches[j].downStream = reaches[i].ID;
                            }
                            else
                            {
                                group.tributary2 = reaches[j].ID;
                                reaches[i].upstream2 = reaches[j].ID;
                                reaches[j].downStream = reaches[i].ID;
                                break;
                            }
                        }
                    }
                    groups.Add(group);
                }
            }
            return groups;
        }
        private void CalInDegree(ref IList<Reach> reaches, int index)
        {
            for (int i = 0; i < reaches.Count; i++)
            {
                //when the start node of the reach equals to the end node of other reach, indegree of the reach plus 1
                if (reaches[index].points[0].GetX() == reaches[i].points[reaches[i].points.Count - 1].GetX()
                   && reaches[index].points[0].GetY() == reaches[i].points[reaches[i].points.Count - 1].GetY())
                {
                    reaches[index].InDeg++;
                }
            }
        }
    }
}
