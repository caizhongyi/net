//---------------------------------------------------------------------------
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Limited Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
// All other rights reserved.
//
// This file is part of the 3D Tools for Windows Presentation Foundation
// project.  For more information, see:
// 
// http://CodePlex.com/Wiki/View.aspx?ProjectName=3DTools
//
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows;
using _3DTools;

namespace PhotoBrowser.Shapes
{
    public class PartialSphere : InteractiveVisual3D
    {
        public PartialSphere(double thetaStart, double thetaWidth, double phiStart, double phiWidth, int tDiv, int pDiv, bool invert)
        {
            Geometry = Tessellate(tDiv, pDiv, Radius, thetaStart, thetaWidth, phiStart, phiWidth, invert);
        }

        private int _thetaDiv = 15;
        public int ThetaDiv
        {
            get { return _thetaDiv; }
            set { _thetaDiv = value; }
        }

        private int _phiDiv = 15;
        public int PhiDiv
        {
            get { return _phiDiv; }
            set { _phiDiv = value; }
        }

        private double _radius = 1;
        public double Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }        

        internal static Point3D GetPosition(double theta, double phi, double radius)
        {
            double x = radius * Math.Sin(theta) * Math.Sin(phi);
            double y = radius * Math.Cos(phi);
            double z = radius * Math.Cos(theta) * Math.Sin(phi);

            return new Point3D(x, y, z);
        }

        private static Vector3D GetNormal(double theta, double phi)
        {
            return (Vector3D)GetPosition(theta, phi, 1.0);
        }

        internal static double DegToRad(double degrees)
        {
            return (degrees / 180.0) * Math.PI;
        }

        private static Point GetTextureCoordinate(double theta, double phi)
        {
            Point p = new Point(theta / (2 * Math.PI),
                                phi / (Math.PI));

            return p;
        }

        internal static MeshGeometry3D Tessellate(int tDiv,
                                                  int pDiv,
                                                  double radius,
                                                  double thetaStart,
                                                  double thetaWidth,
                                                  double phiStart,
                                                  double phiWidth,
                                                  bool invert)
        {
            double dt = DegToRad(thetaWidth) / tDiv;
            double dp = DegToRad(phiWidth) / pDiv;

            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int pi = 0; pi <= pDiv; pi++)
            {
                double phi = DegToRad(phiStart) + pi * dp;

                for (int ti = 0; ti <= tDiv; ti++)
                {
                    // we want to start the mesh on the x axis
                    double theta = DegToRad(thetaStart) + ti * dt;

                    mesh.Positions.Add(GetPosition(theta, phi, radius));
                    mesh.Normals.Add(GetNormal(theta, phi));
                    mesh.TextureCoordinates.Add(GetTextureCoordinate(Math.Abs(theta - DegToRad(thetaStart)) / Math.Abs(DegToRad(thetaWidth)) * 2 * Math.PI, 
                                                                     Math.Abs(phi - DegToRad(phiStart)) / Math.Abs(DegToRad(phiWidth)) * Math.PI));
                }
            }

            for (int pi = 0; pi < pDiv; pi++)
            {
                for (int ti = 0; ti < tDiv; ti++)
                {
                    int x0 = ti;
                    int x1 = (ti + 1);
                    int y0 = pi * (tDiv + 1);
                    int y1 = (pi + 1) * (tDiv + 1);

                    if (invert)
                    {
                        mesh.TriangleIndices.Add(x0 + y0);
                        mesh.TriangleIndices.Add(x1 + y0);
                        mesh.TriangleIndices.Add(x0 + y1);

                        mesh.TriangleIndices.Add(x1 + y0);
                        mesh.TriangleIndices.Add(x1 + y1);
                        mesh.TriangleIndices.Add(x0 + y1);
                    }
                    else
                    {
                        mesh.TriangleIndices.Add(x0 + y0);
                        mesh.TriangleIndices.Add(x0 + y1);
                        mesh.TriangleIndices.Add(x1 + y0);

                        mesh.TriangleIndices.Add(x1 + y0);
                        mesh.TriangleIndices.Add(x0 + y1);
                        mesh.TriangleIndices.Add(x1 + y1);
                    }
                }
            }

            mesh.Freeze();
            return mesh;
        }
    }
}
