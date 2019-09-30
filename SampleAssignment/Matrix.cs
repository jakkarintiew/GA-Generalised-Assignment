
namespace SimpleAssignment
{
    public class Matrix
    {
        private int[,] _flow;        // flow between facilities
        private int[,] _distance;    // distance between locations
        private int[,] _cost;

        public Matrix(int size, int[,] flow, int[,] distance)
        {
            InitDistanceMatrix(distance);
            InitFlowMatrix(flow);
            _cost = new int[size, size];
        }

        public void InitDistanceMatrix(int[,] array)
        {
            _distance = array;
        }

        public void InitFlowMatrix(int[,] array)
        {
            _flow = array;
        }

        private int IsFacilityAtLocation(int facility_i, int location_j, Chromosome chromosome)
        {
            var facility = chromosome.GetFacility(location_j);

            return facility == facility_i ? 1 : 0;
        }

        public int GetChromosomeCost(Chromosome chromosome, bool maximise)
        {
            var totalCost = 0;

            var size = chromosome.Size();

            for (var i = 0; i < size; ++i)
            {
                for (var j = 0; j < size; ++j)
                {
                    var fi = chromosome.GetLocation(i);
                    var fj = chromosome.GetLocation(j);

                    var flow = _flow[i, j];
                    var distance = _distance[fi, fj];

                    totalCost += distance * flow;
                }
            }

            return totalCost;
        }

        public int GetCost(int factory, int task)
        {
            return _cost[factory, task];
        }
    }
}
