using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace GameEngine
{
    class DrawConnection
    {
        public Path ConnectionPath { get; set; }
        public DrawConnection(double startX, double startY, double endX, double endY)
        {
            double tension = 0.0;
            ConnectionPath = DrawCurve(tension, startX, startY, endX, endY);
        }
        public void update(double startX, double startY, double endX, double endY)
        {
            double tension = 1;
            ConnectionPath = DrawCurve(tension, startX, startY, endX, endY);
        }
        public static Path DrawCurve(double tension, double startX, double startY, double endX, double endY)
        {
            // Remove any previous curves.
            //canDrawing.Children.Clear();

            // Make a path.
            Point[] points1 =
            {
                new Point(startX, startY),
                new Point(MyMath.Lerp(startX,endX, 0.05), MyMath.Lerp(startY,endY, 0.3)),
                new Point(MyMath.Lerp(startX,endX, 0.95), MyMath.Lerp(startY,endY, 0.7)),
                new Point(endX, endY),
            };
            Path path1 = MakeCurve(points1, tension);
            path1.Stroke = Brushes.LightGreen;
            path1.StrokeThickness = 2;
            //canDrawing.Children.Add(path1);

            //foreach (Point point in points1)
            //{
            //    Rectangle rect = new Rectangle();
            //    rect.Width = 6;
            //    rect.Height = 6;
            //    Canvas.SetLeft(rect, point.X - 3);
            //    Canvas.SetTop(rect, point.Y - 3);
            //    rect.Fill = Brushes.White;
            //    rect.Stroke = Brushes.Black;
            //    rect.StrokeThickness = 1;
            //    canDrawing.Children.Add(rect);
            //}
            return path1;
        }

        // Make a Path holding a series of Bezier curves.
        // The points parameter includes the points to visit
        // and the control points.
        private static Path MakeBezierPath(Point[] points)
        {
            // Create a Path to hold the geometry.
            Path path = new Path();

            // Add a PathGeometry.
            PathGeometry path_geometry = new PathGeometry();
            path.Data = path_geometry;

            // Create a PathFigure.
            PathFigure path_figure = new PathFigure();
            path_geometry.Figures.Add(path_figure);

            // Start at the first point.
            path_figure.StartPoint = points[0];

            // Create a PathSegmentCollection.
            PathSegmentCollection path_segment_collection =
                new PathSegmentCollection();
            path_figure.Segments = path_segment_collection;

            // Add the rest of the points to a PointCollection.
            PointCollection point_collection =
                new PointCollection(points.Length - 1);
            for (int i = 1; i < points.Length; i++)
                point_collection.Add(points[i]);
            
            // Make a PolyBezierSegment from the points.
            PolyBezierSegment bezier_segment = new PolyBezierSegment();
            bezier_segment.Points = point_collection;

            // Add the PolyBezierSegment to othe segment collection.
            path_segment_collection.Add(bezier_segment);

            return path;
        }

        // Make an array containing Bezier curve points and control points.
        private static Point[] MakeCurvePoints(Point[] points, double tension)
        {
            if (points.Length < 2) return null;
            double control_scale = tension / 0.5 * 0.175;

            // Make a list containing the points and
            // appropriate control points.
            List<Point> result_points = new List<Point>();
            result_points.Add(points[0]);

            for (int i = 0; i < points.Length - 1; i++)
            {
                // Get the point and its neighbors.
                Point pt_before = points[Math.Max(i - 1, 0)];
                Point pt = points[i];
                Point pt_after = points[i + 1];
                Point pt_after2 = points[Math.Min(i + 2, points.Length - 1)];

                double dx1 = pt_after.X - pt_before.X;
                double dy1 = pt_after.Y - pt_before.Y;

                Point p1 = points[i];
                Point p4 = pt_after;

                double dx = pt_after.X - pt_before.X;
                double dy = pt_after.Y - pt_before.Y;
                Point p2 = new Point(
                    pt.X + control_scale * dx,
                    pt.Y + control_scale * dy);

                dx = pt_after2.X - pt.X;
                dy = pt_after2.Y - pt.Y;
                Point p3 = new Point(
                    pt_after.X - control_scale * dx,
                    pt_after.Y - control_scale * dy);

                // Save points p2, p3, and p4.
                result_points.Add(p2);
                result_points.Add(p3);
                result_points.Add(p4);
            }

            // Return the points.
            return result_points.ToArray();
        }

        // Make a Bezier curve connecting these points.
        private static Path MakeCurve(Point[] points, double tension)
        {
            if (points.Length < 2) return null;
            Point[] result_points = MakeCurvePoints(points, tension);

            // Use the points to create the path.
            return MakeBezierPath(result_points.ToArray());
        }

        // Update the tension and rebuild the curve.
        //private void scrTension_Scroll(object sender, ScrollEventArgs e)
        //{
        //    // Get the tension.
        //    double tension = scrTension.Value / 10;
        //    lblTension.Content = tension.ToString("0.0");

        //    // Rebuild the curve.
        //    DrawCurve(tension);
        //}
    }
}
