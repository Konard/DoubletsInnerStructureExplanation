using Platform.Collections.Lists;
using Platform.Data.Doublets;
using Platform.Data.Doublets.Memory;
using Platform.Data.Doublets.Memory.United;
using Platform.Data.Doublets.Memory.United.Generic;
using Platform.IO;
using System.IO;

namespace DoubletsInnerStructureExplanation
{
    class Program
    {
        static void Main()
        {
            string file = "db.links";

            File.Delete(file);

            ulong a; // (1: 1 1)
            using (var linksMemory = new UnitedMemoryLinks<ulong>(file))
            {
                a = linksMemory.CreatePoint();
            }

            var header = FileHelpers.ReadFirstOrDefault<LinksHeader<ulong>>(file);
            var links = FileHelpers.ReadAll<RawLink<ulong>>(file).SkipFirst();

            ulong b; // (2: 2 2)
            using (var linksMemory = new UnitedMemoryLinks<ulong>(file))
            {
                b = linksMemory.CreatePoint();
            }

            header = FileHelpers.ReadFirstOrDefault<LinksHeader<ulong>>(file);
            links = FileHelpers.ReadAll<RawLink<ulong>>(file).SkipFirst();

            ulong c; // (3: 3 3)
            using (var linksMemory = new UnitedMemoryLinks<ulong>(file))
            {
                c = linksMemory.CreatePoint();
            }

            header = FileHelpers.ReadFirstOrDefault<LinksHeader<ulong>>(file);
            links = FileHelpers.ReadAll<RawLink<ulong>>(file).SkipFirst();

            ulong aAndBPair; // (4: A B)
            using (var linksMemory = new UnitedMemoryLinks<ulong>(file))
            {
                aAndBPair = linksMemory.CreateAndUpdate(a, b);
            }

            header = FileHelpers.ReadFirstOrDefault<LinksHeader<ulong>>(file);
            links = FileHelpers.ReadAll<RawLink<ulong>>(file).SkipFirst();

            ulong abcSequence; // (5: (4: A B) C)
            using (var linksMemory = new UnitedMemoryLinks<ulong>(file))
            {
                abcSequence = linksMemory.CreateAndUpdate(aAndBPair, c);
            }

            header = FileHelpers.ReadFirstOrDefault<LinksHeader<ulong>>(file);
            links = FileHelpers.ReadAll<RawLink<ulong>>(file).SkipFirst();
        }
    }
}
