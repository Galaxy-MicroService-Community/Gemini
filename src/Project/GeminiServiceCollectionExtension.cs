using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GeminiServiceCollectionExtension
    {
        private static bool _done;
        private static readonly object _lock;
        private readonly static MethodInfo _configWithNameMethodInfo;
        private readonly static MethodInfo _configWithoutNameMethodInfo;
        static GeminiServiceCollectionExtension()
        {
            _done = false;
            _lock = new object();
            _configWithNameMethodInfo = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new Type[] { typeof(IServiceCollection), typeof(string), typeof(IConfiguration) })!;
            _configWithoutNameMethodInfo = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", new Type[] { typeof(IServiceCollection), typeof(IConfiguration) })!;
        }


        /// <summary>
        /// 需要被使用, 用于注册所有 Gemini 选项实体
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">配置实体</param>
        public static IServiceCollection AddGeminiBuilder<TBuilder>(this IServiceCollection services, Func<TBuilder, TBuilder>? builderAction = null)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.Configure<GeminiAttribute>(configuration.GetSection(""));
            //TBuilder builder = new();
            //BuilderCache.Add(builder);
            //builderAction?.Invoke(builder);
            //builder.InjectServices(services);
            return services;
        }


        /// <summary>
        /// 添加自动选项
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGeminiOptions(this IServiceCollection services)
        {

            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            if (!_done)
            {
                lock (_lock)
                {
                    if (!_done)
                    {
                        _done = true;
                        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                        foreach (var asm in assemblies)
                        {
                            if (!asm.IsDynamic)
                            {
                                try
                                {
                                    Type[] types = asm.GetTypes();
                                    foreach (var type in types)
                                    {

                                        var optionsAttr = type.GetCustomAttribute<GeminiAttribute>();
                                        if (optionsAttr != default)
                                        {
                                            string[] positions = optionsAttr.FullPositions;
                                            for (int i = 0; i < positions.Length; i += 1)
                                            {

                                                var key = positions[i];
#if DEBUG
                                                Console.WriteLine($"注册配置 [{key}] 节点类型为 {type.Name}!");
#endif
                                                //GeminiContext.OptionsTypeCache[type] = key;
                                                var methodInfo = _configWithNameMethodInfo.MakeGenericMethod(type)!;
                                                methodInfo.Invoke(null, new object[] { services, key, configuration.GetSection(key) });

                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            return services;
        }
    }

}
