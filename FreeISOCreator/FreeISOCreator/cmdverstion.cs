using Export;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FreeISOCreator
{
    internal class cmdverstion
    {
 

        public void CmdVersion(string folderPath, string outputPath, string VMname)

        {

            IsoCreator.IsoCreator isoCreator = new IsoCreator.IsoCreator();
            isoCreator.Folder2Iso(folderPath, outputPath, VMname);
            
            Console.WriteLine("Done");

           

        }
        

    }
}
