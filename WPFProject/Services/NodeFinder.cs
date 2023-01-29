using System.Collections.Generic;
using System.Xml;
using WPFProject.Data;

namespace WPFProject.Services
{
    /// <summary>
    /// Представляет сервис определению нод, потомками которых являются item.
    /// </summary>
    internal class NodeFinder : XmlTransformer
    {
        #region Методы

        /// <summary>
        /// Добавляет в коллекцию <paramref name="collection"/> ноды, потомками которых являются элементы item.
        /// </summary>
        /// <param name="path"> Путь к xml документу. </param>
        /// <param name="collection"> Коллекция для добавления. </param>
        public void SetItems(string path, ICollection<Node> collection)
        {
            var doc = new XmlDocument();
            doc.Load(path);

            var allNodesForChildCount = GetAllNodesForChildCount(doc);

            foreach (var node in allNodesForChildCount)
            {
                int childCount = node.ChildNodes.Count;

                string nodeName;

                if (node.Attributes.Count > 0)
                    nodeName = node.Attributes[0].Value;
                else
                    nodeName = node.Name;

                collection.Add( new Node { Name = nodeName, ChildrensCount = childCount });
            }
        }

        #endregion Методы
    }
}
