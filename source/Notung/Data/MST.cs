﻿using System;
using System.Collections.Generic;
using System.Linq;
using Notung.Properties;

namespace Notung.Data
{
  /// <summary>
  /// Методы построения минимального остовного дерева
  /// </summary>
  public static class MST
  {
    #region Kruskal algorithm ---------------------------------------------------------------------

    private struct ConnectivityComponent
    {
      public int ID, Count;
    }

    private static int FindRoot(this ConnectivityComponent[] components, int peak)
    {
      while (components[peak].ID != peak)
        peak = components[peak].ID;

      return peak;
    }

    private static void Merge(this ConnectivityComponent[] components, int peak1, int peak2)
    {
      if (components[peak1].ID == components[peak2].ID)
        return;

      if (components[peak1].Count > components[peak2].Count)
      {
        components[peak2].ID = components[peak1].ID;
        components[peak1].Count += components[peak2].Count;
      }
      else
      {
        components[peak1].ID = components[peak2].ID;
        components[peak2].Count += components[peak1].Count;
      }
    }

    /// <summary>
    /// Алгоритм Краскала
    /// </summary>
    /// <typeparam name="T">Тип веса ребра</typeparam>
    /// <param name="graph">Граф, в котором ищется минимальное остовное дерево</param>
    /// <returns>Массив рёбер в порядке возрастания веса, составляющих минимальное остовное дерево</returns>
    public static Tuple<int, int, T>[] Kruskal<T>(IWeightedGraph<T> graph) where T : IComparable<T>
    {
      if (graph == null)
        throw new ArgumentNullException("graph");

      // Все рёбра, отсортированные по возрастанию веса
      var all_arcs = (from i in Enumerable.Range(0, graph.PeakCount)
                      from a in graph.OutgoingArcs(i)
                      .Where(a => graph.IsOriented || i < a.Item1).Select(a => new
                      {
                        From = i,
                        To = a.Item1,
                        Weight = a.Item2
                      })
                      select a).OrderBy(a => a.Weight);

      var components = new ConnectivityComponent[graph.PeakCount];
      var result = new Tuple<int, int, T>[graph.PeakCount - 1];
      var arcs_added = 0;

      for (int i = 0; i < components.Length; i++)
      {
        components[i].ID = i;
        components[i].Count = 1;
      }

      foreach (var arc in all_arcs)
      {
        var root1 = components.FindRoot(arc.From);
        var root2 = components.FindRoot(arc.To);

        if (root1 != root2)
        {
          components.Merge(root1, root2);
          result[arcs_added++] = new Tuple<int, int, T>(arc.From, arc.To, arc.Weight);
        }
      }

      if (arcs_added < graph.PeakCount - 1)
        throw new ArgumentException(Resources.GRAPH_DISCONNECTED);

      return result;
    }

    #endregion

    #region Prim algorithm ------------------------------------------------------------------------

    private struct ArcInfo<T>
    {
      public int From;
      public int To;
      public T Weight;

      public bool Empty
      {
        get { return From == 0; }
      }
    }

    private static ArcInfo<T> FindMinimum<T>(this ArcInfo<T>[] arcs, HashSet<int> unprocessed)
      where T : IComparable<T>
    {
      ArcInfo<T> min = default(ArcInfo<T>);

      foreach (var peak in unprocessed)
      {
        if (!unprocessed.Contains(arcs[peak].To) && !arcs[peak].Empty)
        {
          if (min.Empty || min.Weight.CompareTo(arcs[peak].Weight) > 0)
            min = arcs[peak];
        }
      }

      return min;
    }

    /// <summary>
    /// Алгоритм Прима
    /// </summary>
    /// <typeparam name="T">Тип веса ребра</typeparam>
    /// <param name="graph">Граф, в котором ищется минимальное остовное дерево</param>
    /// <returns>Массив рёбер, составляющих минимальное остовное дерево</returns>
    public static Tuple<int, int, T>[] Prim<T>(IWeightedGraph<T> graph) where T : IComparable<T>
    {
      if (graph == null)
        throw new ArgumentNullException("graph");

      if (graph.PeakCount == 0)
        return ArrayExtensions.Empty<Tuple<int, int, T>>();

      // Вершина 0  с самого начала считается обработанной
      var unprocessed = new HashSet<int>(Enumerable.Range(1, graph.PeakCount - 1));
      var min_arcs = new ArcInfo<T>[graph.PeakCount];
      var result = new Tuple<int, int, T>[graph.PeakCount - 1];
      var arcs_added = 0;

      // Все рёбра, ведущие в вершину 0
      foreach (var arc in graph.IncomingArcs(0))
        min_arcs[arc.Item1] = new ArcInfo<T> { From = arc.Item1, To = 0, Weight = arc.Item2 };

      while (unprocessed.Count > 0)
      {
        ArcInfo<T> min = min_arcs.FindMinimum<T>(unprocessed);

        if (!min.Empty)
        {
          result[arcs_added++] = new Tuple<int, int, T>(
            Math.Min(min.From, min.To), Math.Max(min.From, min.To), min.Weight);

          unprocessed.Remove(min.From);

          if (graph.IncomingCount(min.From) < unprocessed.Count)
          {
            foreach (var arc in graph.IncomingArcs(min.From))
            {
              if (!unprocessed.Contains(arc.Item1))
                continue;

              if (min_arcs[arc.Item1].Empty || arc.Item2.CompareTo(min_arcs[arc.Item1].Weight) < 0)
              {
                min_arcs[arc.Item1] = new ArcInfo<T>
                {
                  From = arc.Item1,
                  To = min.From,
                  Weight = arc.Item2
                };
              }
            }
          }
          else
          {
            foreach (int peak in unprocessed)
            {
              if (graph.HasArc(min.From, peak))
              {
                T weight = graph[min.From, peak];

                if (min_arcs[peak].Empty || weight.CompareTo(min_arcs[peak].Weight) < 0)
                  min_arcs[peak] = new ArcInfo<T> { From = peak, To = min.From, Weight = weight };
              }
            }
          }
        }
        else
          throw new ArgumentException(Resources.GRAPH_DISCONNECTED);
      }

      return result;
    }

    #endregion
  }
}