﻿/******************************************************************************/
/*
  Project - Unity CJ Lib
            https://github.com/TheAllenChou/unity-cj-lib
  
  Author  - Ming-Lun "Allen" Chou
  Web     - http://AllenChou.net
  Twitter - @TheAllenChou
*/
/******************************************************************************/

using UnityEngine;

namespace CjLib
{
  public class DebugUtil
  {

    // box
    // ------------------------------------------------------------------------

    public static void DrawBox(Vector3 center, Vector3 dimensions, Quaternion rotation, Color color, float duration = 0.0f, bool depthTest = true)
    {
      Vector3 extents = 0.5f * dimensions;
      Vector3 offsetX = rotation * (extents.x * Vector3.right);
      Vector3 offsetY = rotation * (extents.y * Vector3.up);
      Vector3 offsetZ = rotation * (extents.z * Vector3.forward);

      Vector3 p0 = center - offsetX - offsetY - offsetZ;
      Vector3 p1 = center - offsetX + offsetY - offsetZ;
      Vector3 p2 = center + offsetX + offsetY - offsetZ;
      Vector3 p3 = center + offsetX - offsetY - offsetZ;
      Vector3 p4 = center - offsetX - offsetY + offsetZ;
      Vector3 p5 = center - offsetX + offsetY + offsetZ;
      Vector3 p6 = center + offsetX + offsetY + offsetZ;
      Vector3 p7 = center + offsetX - offsetY + offsetZ;

      Debug.DrawLine(p0, p1, color, duration, depthTest);
      Debug.DrawLine(p1, p2, color, duration, depthTest);
      Debug.DrawLine(p2, p3, color, duration, depthTest);
      Debug.DrawLine(p3, p0, color, duration, depthTest);

      Debug.DrawLine(p2, p6, color, duration, depthTest);
      Debug.DrawLine(p6, p7, color, duration, depthTest);
      Debug.DrawLine(p7, p3, color, duration, depthTest);

      Debug.DrawLine(p7, p4, color, duration, depthTest);
      Debug.DrawLine(p4, p5, color, duration, depthTest);
      Debug.DrawLine(p5, p6, color, duration, depthTest);

      Debug.DrawLine(p5, p1, color, duration, depthTest);
      Debug.DrawLine(p1, p0, color, duration, depthTest);
      Debug.DrawLine(p0, p4, color, duration, depthTest);
    }

    // ------------------------------------------------------------------------
    // end: box
    

    // circle
    // ------------------------------------------------------------------------

    public static void DrawCircle(Vector3 center, Vector3 normal, float radius, int numSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      if (numSegments <= 1)
        return;

      Vector3 binormal = Vector3.Dot(normal, Vector3.up) < 0.5f ? Vector3.up : Vector3.forward;
      Vector3 axisX = Vector3.Normalize(Vector3.Cross(normal, binormal));
      Vector3 axisZ = Vector3.Cross(normal, axisX);
      Vector3 baseX = radius * axisX;
      Vector3 baseZ = radius * axisZ;

      float angleIncrement = 2.0f * Mathf.PI / numSegments;
      float angle = 0.0f;
      Vector3 prevPos = center + baseX;
      for (int i = 0; i < numSegments; ++i)
      {
        angle += angleIncrement;
        Vector3 currPos = center + Mathf.Cos(angle) * baseX + Mathf.Sin(angle) * baseZ;
        Debug.DrawLine(prevPos, currPos, color, duration, depthTest);
        prevPos = currPos;
      }
    }

    public static void DrawCircle2D(Vector3 center, float radius, int numSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      if (numSegments <= 1)
        return;

      Vector3 axisX = Vector3.right;
      Vector3 axisY = Vector3.up;
      Vector3 baseX = radius * axisX;
      Vector3 baseY = radius * axisY;

      float angleIncrement = 2.0f * Mathf.PI / numSegments;
      float angle = 0.0f;
      Vector3 prevPos = center + baseX;
      for (int i = 0; i < numSegments; ++i)
      {
        angle += angleIncrement;
        Vector3 currPos = center + Mathf.Cos(angle) * baseX + Mathf.Sin(angle) * baseY;
        Debug.DrawLine(prevPos, currPos, color, duration, depthTest);
        prevPos = currPos;
      }
    }

    // ------------------------------------------------------------------------
    // end: circle


    // cylinder
    // ------------------------------------------------------------------------

    public static void DrawCylinder(Vector3 point0, Vector3 point1, float radius, int numSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      if (numSegments <= 1)
        return;

      Vector3 axisY = point1 - point0;
      float axisYSelfDot = Vector3.Dot(axisY, axisY);
      if (axisYSelfDot < MathUtil.kEpsilon)
        return;

      axisY = Vector3.Normalize(axisY);
      Vector3 axisYPerp = Vector3.Dot(axisY, Vector3.up) < 0.5f ? Vector3.up : Vector3.forward;
      Vector3 axisX = Vector3.Normalize(Vector3.Cross(axisY, axisYPerp));
      Vector3 axisZ = Vector3.Cross(axisY, axisX);
      Vector3 baseX = radius * axisX;
      Vector3 baseZ = radius * axisZ;

      float angleIncrement = 2.0f * Mathf.PI / numSegments;
      float angle = 0.0f;
      Vector3 prevPos0 = point0 + baseX;
      Vector3 prevPos1 = point1 + baseX;
      for (int i = 0; i < numSegments; ++i)
      {
        angle += angleIncrement;
        Vector3 offset = Mathf.Cos(angle) * baseX + Mathf.Sin(angle) * baseZ;
        Vector3 currPos0 = point0 + offset;
        Vector3 currPos1 = point1 + offset;
        Debug.DrawLine(currPos0, currPos1, color, duration, depthTest);
        Debug.DrawLine(currPos0, prevPos0, color, duration, depthTest);
        Debug.DrawLine(currPos1, prevPos1, color, duration, depthTest);
        prevPos0 = currPos0;
        prevPos1 = currPos1;
      }
    }

    // ------------------------------------------------------------------------
    // cylinder


    // sphere
    // ------------------------------------------------------------------------

    public static void DrawSphereTripleCircles(Vector3 center, Quaternion rotation, float radius, int numSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      Vector3 axisX = rotation * Vector3.right;
      Vector3 axisY = rotation * Vector3.up;
      Vector3 axisZ = rotation * Vector3.forward;
      DrawCircle(center, axisX, radius, numSegments, color, duration, depthTest);
      DrawCircle(center, axisY, radius, numSegments, color, duration, depthTest);
      DrawCircle(center, axisZ, radius, numSegments, color, duration, depthTest);
    }

    // identity rotation
    public static void DrawSphereTripleCircles(Vector3 center, float radius, int numSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      DrawSphereTripleCircles(center, Quaternion.identity, radius, numSegments, color, duration, depthTest);
    }

    public static void DrawSphere(Vector3 center, Quaternion rotation, float radius, int latSegments, int longSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      if (latSegments <= 0 || longSegments <= 1)
        return;

      Vector3 axisX = rotation * Vector3.right;
      Vector3 axisY = rotation * Vector3.up;
      Vector3 axisZ = rotation * Vector3.forward;

      Vector3 top = center + radius * axisY;
      Vector3 bottom = center - radius * axisY;

      float[] aLatSin = new float[latSegments];
      float[] aLatCos = new float[latSegments];
      {
        float latAngleIncrement = Mathf.PI / (latSegments + 1);
        float latAngle = 0.0f;
        for (int iLat = 0; iLat < latSegments; ++iLat)
        {
          latAngle += latAngleIncrement;
          aLatSin[iLat] = Mathf.Sin(latAngle);
          aLatCos[iLat] = Mathf.Cos(latAngle);
        }
      }

      float[] aLongSin = new float[longSegments];
      float[] aLongCos = new float[longSegments];
      {
        float longAngleIncrement = 2.0f * Mathf.PI / longSegments;
        float longAngle = 0.0f;
        for (int iLong = 0; iLong < longSegments; ++iLong)
        {
          longAngle += longAngleIncrement;
          aLongSin[iLong] = Mathf.Sin(longAngle);
          aLongCos[iLong] = Mathf.Cos(longAngle);
        }
      }

      Vector3[] aPrevLongSample = new Vector3[latSegments];
      for (int iLat = 0; iLat < latSegments; ++iLat)
      {
        float latSin = aLatSin[iLat];
        float latCos = aLatCos[iLat];
        aPrevLongSample[iLat] = center + radius * (axisX * latSin + axisY * latCos);
      }

      Vector3[] aCurrLongSample = new Vector3[latSegments];
      for (int iLong = 0; iLong < longSegments; ++iLong)
      {
        float longSin = aLongSin[iLong];
        float longCos = aLongCos[iLong];

        for (int iLat = 0; iLat < latSegments; ++iLat)
        {
          float latSin = aLatSin[iLat];
          float latCos = aLatCos[iLat];
          Vector3 vecXz = axisX * longCos + axisZ * longSin;
          aCurrLongSample[iLat] = center + radius * (vecXz * latSin + axisY * latCos);
        }

        Debug.DrawLine(top, aCurrLongSample[0], color, duration, depthTest);
        for (int iLat = 0; iLat < latSegments; ++iLat)
        {
          if (iLat < latSegments - 1)
          {
            Debug.DrawLine(aCurrLongSample[iLat], aCurrLongSample[iLat + 1], color, duration, depthTest);
          }
          Debug.DrawLine(aCurrLongSample[iLat], aPrevLongSample[iLat], color, duration, depthTest);
        }
        Debug.DrawLine(aCurrLongSample[latSegments - 1], bottom);

        Vector3[] aLongTempSample = aPrevLongSample;
        aPrevLongSample = aCurrLongSample;
        aCurrLongSample = aLongTempSample;
      }
    }

    // identity rotation
    public static void DrawSphere(Vector3 center, float radius, int latSegments, int longSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      DrawSphere(center, Quaternion.identity, radius, latSegments, longSegments, color, duration, depthTest);
    }

    // ------------------------------------------------------------------------
    // end: sphere


    // capsule
    // ------------------------------------------------------------------------

    public static void DrawCapsule(Vector3 point0, Vector3 point1, float radius, int latSegments, int longSegments, Color color, float duration = 0.0f, bool depthTest = true)
    {
      if (latSegments <= 0 || longSegments <= 1)
        return;

      Vector3 axisY = point1 - point0;
      float axisYSelfDot = Vector3.Dot(axisY, axisY);
      if (axisYSelfDot < MathUtil.kEpsilon)
        return;

      axisY = Vector3.Normalize(axisY);
      Vector3 axisYPerp = Vector3.Dot(axisY, Vector3.up) < 0.5f ? Vector3.up : Vector3.forward;
      Vector3 axisX = Vector3.Normalize(Vector3.Cross(axisY, axisYPerp));
      Vector3 axisZ = Vector3.Cross(axisY, axisX);

      Vector3 end0 = point0 - radius * axisY;
      Vector3 end1 = point1 + radius * axisY;

      latSegments = latSegments * 2 - 1;
      float[] aLatSin = new float[latSegments];
      float[] aLatCos = new float[latSegments];
      {
        float latAngleIncrement = Mathf.PI / (latSegments + 1);
        float latAngle = 0.0f;
        for (int iLat = 0; iLat < latSegments; ++iLat)
        {
          latAngle += latAngleIncrement;
          aLatSin[iLat] = Mathf.Sin(latAngle);
          aLatCos[iLat] = Mathf.Cos(latAngle);
        }
      }

      float[] aLongSin = new float[longSegments];
      float[] aLongCos = new float[longSegments];
      {
        float longAngleIncrement = 2.0f * Mathf.PI / longSegments;
        float longAngle = 0.0f;
        for (int iLong = 0; iLong < longSegments; ++iLong)
        {
          longAngle += longAngleIncrement;
          aLongSin[iLong] = Mathf.Sin(longAngle);
          aLongCos[iLong] = Mathf.Cos(longAngle);
        }
      }

      Vector3[] aPrevLongOffset = new Vector3[latSegments];
      for (int iLat = 0; iLat < latSegments; ++iLat)
      {
        float latSin = aLatSin[iLat];
        float latCos = aLatCos[iLat];
        aPrevLongOffset[iLat] = radius * (axisX * latSin + axisY * latCos);
      }

      Vector3[] aCurrLongOffset = new Vector3[latSegments];
      for (int iLong = 0; iLong < longSegments; ++iLong)
      {
        float longSin = aLongSin[iLong];
        float longCos = aLongCos[iLong];

        for (int iLat = 0; iLat < latSegments; ++iLat)
        {
          float latSin = aLatSin[iLat];
          float latCos = aLatCos[iLat];
          Vector3 vecXz = axisX * longCos + axisZ * longSin;
          aCurrLongOffset[iLat] = radius * (vecXz * latSin + axisY * latCos);
        }

        Debug.DrawLine(end1, point1 + aCurrLongOffset[0], color, duration, depthTest);
        for (int iLat = 0; iLat <= latSegments / 2; ++iLat)
        {
          if (iLat < latSegments / 2)
          {
            Debug.DrawLine(point1 + aCurrLongOffset[iLat], point1 + aCurrLongOffset[iLat + 1], color, duration, depthTest);
          }
          Debug.DrawLine(point1 + aCurrLongOffset[iLat], point1 + aPrevLongOffset[iLat], color, duration, depthTest);
        }
        Debug.DrawLine(point1 + aCurrLongOffset[latSegments / 2], point0 + aCurrLongOffset[latSegments / 2], color, duration, depthTest);
        for (int iLat = latSegments / 2; iLat < latSegments; ++iLat)
        {
          if (iLat < latSegments - 1)
          {
            Debug.DrawLine(point0 + aCurrLongOffset[iLat], point0 + aCurrLongOffset[iLat + 1], color, duration, depthTest);
          }
          Debug.DrawLine(point0 + aCurrLongOffset[iLat], point0 + aPrevLongOffset[iLat], color, duration, depthTest);
        }
        Debug.DrawLine(point0 + aCurrLongOffset[latSegments - 1], end0);

        Vector3[] aLongTempOffset = aPrevLongOffset;
        aPrevLongOffset = aCurrLongOffset;
        aCurrLongOffset = aLongTempOffset;
      }
    }

    // ------------------------------------------------------------------------
    // end: capsule
  }
}
