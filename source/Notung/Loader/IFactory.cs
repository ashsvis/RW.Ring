﻿using System;
using System.Reflection;

namespace Notung.Loader
{
  /// <summary>
  /// Фабрика для отложенной загрузки
  /// </summary>
  /// <typeparam name="T">Тип порождаемого объекта</typeparam>
  public interface IFactory<T>
  {
    T Create();
  }

  /// <summary>
  /// Фабрика, создающая объект указанного типа вызовом конструктора без параметров
  /// </summary>
  /// <typeparam name="TContract">Требуемый тип данных</typeparam>
  /// <typeparam name="TService">Тип объекта, реально порождаемый фабрикой</typeparam>
  public sealed class DefaultFactory<TContract, TService> : IFactory<TContract>
    where TContract : class
    where TService : TContract, new()
  {
    public static readonly IFactory<TContract> Instance = new DefaultFactory<TContract, TService>();

    private DefaultFactory() { }

    public TContract Create()
    {
      return new TService();
    }
  }

  /// <summary>
  /// Фабрика, которая всегда возвращает null
  /// </summary>
  public sealed class EmptyFactory<T> : IFactory<T> where T : class
  {
    public static readonly IFactory<T> Instance = new EmptyFactory<T>();

    private EmptyFactory() { }
    
    public T Create() { return null; }
  }

  /// <summary>
  /// Фабрика, которую можно создать, не загружая сборку, 
  /// в которой описан тип реально порождаемого объекта
  /// </summary>
  /// <typeparam name="T">Требуемый тип данных</typeparam>
  public sealed class DeferredFactory<T> : IFactory<T>
  {
    private readonly string m_assembly;
    private readonly string m_type;

    /// <summary>
    /// Инициализация фабрики
    /// </summary>
    /// <param name="assembly">Имя сборки, в которой объявлен искомый тип</param>
    /// <param name="type">Полное имя типа данный, который будет порождать фабрика</param>
    public DeferredFactory(string assembly, string type)
    {
      if (string.IsNullOrEmpty(assembly))
        throw new ArgumentNullException("assembly");

      if (string.IsNullOrEmpty(type))
        throw new ArgumentNullException("type");

      m_assembly = assembly;
      m_type = type;
    }
    
    public T Create()
    {
      Assembly asm = Assembly.Load(m_assembly);

      Type type = asm.GetType(m_type, true);

      if (typeof(T).IsAssignableFrom(type))
        return (T)Activator.CreateInstance(type);
      else
        throw new TypeLoadException();
    }
  }
}
