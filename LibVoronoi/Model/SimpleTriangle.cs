namespace LibVoronoiDiagram_ArcGISDesktop.Model
{
    /// <summary>
    /// Declare a simple triangle struct
    /// </summary>
    internal class SimpleTriangle
    {
        /// <summary>
        /// Index entries to each vertex
        /// </summary>
        internal int A, B, C;

        /// <summary>
        /// x, y of the centre, and radius of the circle
        /// </summary>
        internal SimplePoint CircumCentre;

        /// <summary>
        /// radius of triangle
        /// </summary>
        internal double Radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleTriangle"/> struct
        /// </summary>
        /// <param name="a">index vertex a</param>
        /// <param name="b">index vertex b</param>
        /// <param name="c">index vertex c</param>
        /// <param name="circumcentre">center of triangle</param>
        /// <param name="radius">radius of triangle</param>
        internal SimpleTriangle(int a, int b, int c, SimplePoint circumcentre, double radius)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.CircumCentre = circumcentre;
            this.Radius = radius;
        }
    }
}
