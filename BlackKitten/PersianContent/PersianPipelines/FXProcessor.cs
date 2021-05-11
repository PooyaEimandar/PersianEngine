//using Microsoft.Xna.Framework.Content.Pipeline;
//using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
//using Microsoft.Xna.Framework.Content.Pipeline.Processors;

//namespace Pipelines
//{
//    [ContentProcessor(DisplayName = "FX Processor")]
//    public class FXProcessor : EffectProcessor
//    {
//        public override CompiledEffectContent Process(EffectContent input, ContentProcessorContext context)
//        {
//            #region Set Debug Mode
                        
//            object obj = null;
//            context.Parameters.TryGetValue("DebugMode", out obj);
//            this.DebugMode = obj != null ? EnumsConverter.StringToEnum<EffectProcessorDebugMode>(obj.ToString()) : EffectProcessorDebugMode.Optimize;
            
//            #endregion

//            #region Set Defines

//            this.Defines = string.Empty;
//            obj = null;
//            context.Parameters.TryGetValue("Defines", out obj);
//            if (obj != null)
//            {
//                this.Defines += obj.ToString();
//            }

//            #endregion

//            return base.Process(input, context);
//        }
//    }
//}
