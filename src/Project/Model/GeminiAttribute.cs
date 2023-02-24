using System;
using System.Collections.Generic;
using System.Reflection;



public sealed class GeminiOptionsAttribute<T> : GeminiAttribute
{
    public GeminiOptionsAttribute(params string[] position) : base(position, typeof(T))
    {

    }
}
public sealed class GeminiOptionsAttribute<T1, T2> : GeminiAttribute
{
    public GeminiOptionsAttribute(params string[] position) : base(position, typeof(T1), typeof(T2))
    {

    }
}
public sealed class GeminiOptionsAttribute<T1, T2, T3> : GeminiAttribute
{
    public GeminiOptionsAttribute(params string[] position) : base(position, typeof(T1), typeof(T2), typeof(T3))
    {

    }
}
public sealed class GeminiOptionsAttribute<T1, T2, T3, T4> : GeminiAttribute
{
    public GeminiOptionsAttribute(params string[] position) : base(position, typeof(T1), typeof(T2), typeof(T3), typeof(T4))
    {

    }
}

public sealed class GeminiOptionsAttribute<T1, T2, T3, T4, T5> : GeminiAttribute
{
    public GeminiOptionsAttribute(params string[] position) : base(position, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5))
    {

    }
}

public sealed class GeminiOptionsAttribute<T1, T2, T3, T4, T5, T6> : GeminiAttribute
{
    public GeminiOptionsAttribute(params string[] position) : base(position, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6))
    {

    }
}
/// <summary>
/// 配置选项标签
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GeminiAttribute : Attribute
{

    public string[] CurrentPosition;
    public string[] FullPositions;

    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="positions">节点名</param>
    public GeminiAttribute(params string[] positions) : this(positions, null) { }

    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="root">根节点名</param>
    /// <param name="parentsType">父节点类型</param>
    public GeminiAttribute(string root, Type parentsType) : this(parentsType, root) { }

    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="parentsType">父节点类型</param>
    /// <param name="positions">节点名</param>
    public GeminiAttribute(Type parentsType, params string[] positions) : this(positions, parentsType) { }

    /// <summary>
    /// 创建选项实体
    /// </summary>
    /// <param name="positions">节点名</param>
    /// <param name="parentsTypes">父节点类型</param>
    public GeminiAttribute(string[] positions, params Type[]? parentsTypes)
    {
        CurrentPosition = positions;
        if (parentsTypes != null)
        {
            List<string> positionList = new();
            for (int typeIndex = 0; typeIndex < parentsTypes.Length; typeIndex += 1)
            {
                var parentsAttr = parentsTypes[typeIndex].GetCustomAttribute<GeminiAttribute>();
                if (parentsAttr != null)
                {

                    for (int i = 0; i < positions.Length; i++)
                    {
                        for (int j = 0; j < parentsAttr.CurrentPosition.Length; j++)
                        {
                            positionList.Add($"{parentsAttr.FullPositions[j]}:{positions[i]}");
                        }
                    }
                }
            }
            FullPositions = positionList.ToArray();
        }
        else
        {
            FullPositions = positions;
        }
    }

}

