﻿using GameEngineLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameEngineVisuals
{
    public class Program
    {
        const int MAINGRID_X = 18;
        const int MAINGRID_Y = 64;
        static char[,] g_cmMainGrid = new char[MAINGRID_X, MAINGRID_Y];
        private static Mutex g_mutex = new Mutex();



        public class MapVertex
        {
            public MapVertex(int iX, int iY)
            {
                m_iX = iX;
                m_iY = iY;
            }
            public int m_iX;
            public int m_iY;
        }


        static List<MapVertex> g_vPosition = new List<MapVertex>();             // TODO: currently there's a bug where the last cordinate/index wont get added.

  
        //-----------------------------------------------------------------------------
        // loadMapFromFile
        //-----------------------------------------------------------------------------		
        // start cordinate index from grid mid to allow for subtraction. ie (iLenght * 0,5).
        public static void loadMapFromFile(MapVertex mapVertex)
        {

            string sContent = File.ReadAllText("Map.bsp"); ;
            char[] cContent = sContent.ToCharArray();
            int iOffset = 0;
            //int iPath;

            int i = 0; // mapVertex.m_iX / 2;   // 5, start at mid
            int j = mapVertex.m_iY / 2;   // 5, start at mid

            //int i = 5;   // 5, start at mid
            //int j = 5;   // 5, start at mid

            for (int x = 0; x != sContent.Length; x++)
            {

                switch (cContent[x])
                {

                    case 'D':
                        //iPath = direction.down;

                        for (int g = 0; g != iOffset; g++)
                        {
                            MapVertex vertex = new MapVertex(i, j);
                            g_vPosition.Add(vertex);
                            i++;

                        }
                        break;

                    case 'U':
                        for (int g = 0; g != iOffset; g++)
                        {
                            MapVertex vertex = new MapVertex(i, j);
                            g_vPosition.Add(vertex);
                            i--;

                        }
                        break;

                    case 'L':
                        //iPath = direction.left;
                        for (int g = 0; g != iOffset; g++)
                        {
                            MapVertex vertex = new MapVertex(i, j);
                            g_vPosition.Add(vertex);
                            j--;

                        }
                        break;

                    case 'R':
                        for (int g = 0; g != iOffset; g++)
                        {
                            MapVertex vertex = new MapVertex(i, j);
                            g_vPosition.Add(vertex);
                            j++;

                        }
                        break;

                    default:                                 // content was a number, ignore that and restart main while loop to check next char.
                        iOffset = (int)Char.GetNumericValue(cContent[x]);

                        //(Convert char to int)
                        continue;
                        //-----------------------------------------------------------------------------		

                }


            }
        }
        //-----------------------------------------------------------------------------
        // Initilize Frame Buffer, 
        //		fills the matrix with specifed symbol, default is 32
        //-----------------------------------------------------------------------------
        static public void initializeFrameBuffer(char cSymbol = (char)32) // heavy, use sparingly
        {
            //memset(cmGrid, ' ', (GRIDSIZE_Y * GRIDSIZE_X));
            for (int j = 0; j < MAINGRID_X; j++)
            {
                for (int i = 0; i < MAINGRID_Y; i++)
                {
                    g_cmMainGrid[j, i] = cSymbol;
                }
            }
        }
        //-----------------------------------------------------------------------------
        // Converts all chars into strings instead in advance. Should run in the background
        //-----------------------------------------------------------------------------
        static void prepareGlobalSendFrameToConsoleStrBuilder()
        {
            while (true)
            {
                //g_mutex.WaitOne();

                g_vCachedPixelBuffer.Clear();

                StringBuilder sb = new StringBuilder();
                for (byte i = 0; i < MAINGRID_X; i++)  // unsgined chars smaller then int
                {
                    sb.Clear();
                    for (byte j = 0; j < MAINGRID_Y; j++)
                    {
                        sb.Append(g_cmMainGrid[i, j]);
                        //Console.Write(g_cmMainGrid[i, j]);
                    }
                    g_vCachedPixelBuffer.Add(sb.ToString());

                    //g_mutex.ReleaseMutex()
                    //Thread.Sleep(2000);
                }
            }
        }
        //-----------------------------------------------------------------------------
        // Draws the grid to the conosle, should only be called after all functions have changed their pixels
        //	 r̶u̶n̶ ̶o̶n̶ ̶e̶x̶t̶e̶r̶n̶a̶l̶ ̶t̶h̶r̶e̶a̶d̶ ̶d̶u̶e̶ ̶t̶o̶ ̶i̶n̶f̶i̶n̶i̶t̶e̶ ̶w̶h̶i̶l̶e̶ ̶l̶o̶o̶p̶
        //	 call this function from the others each time something happens. pixels only affect themselvs 
        //-----------------------------------------------------------------------------
        static List<string> prepareSendFrameToConsoleStrBuilder()
        {

            List<String> vCachedPixelBuffer = new List<string>();


            //Console.Clear();
            StringBuilder sb = new StringBuilder();
            for (byte i = 0; i < MAINGRID_X; i++)  // unsgined chars smaller then int
            {
                sb.Clear();
                for (byte j = 0; j < MAINGRID_Y; j++)
                {
                    sb.Append(g_cmMainGrid[i, j]);
                    //Console.Write(g_cmMainGrid[i, j]);
                }
                vCachedPixelBuffer.Add(sb.ToString());
            }
            return vCachedPixelBuffer;
        }

        //-----------------------------------------------------------------------------
        // Draws the grid to the conosle, should only be called after all functions have changed their pixels
        //	 r̶u̶n̶ ̶o̶n̶ ̶e̶x̶t̶e̶r̶n̶a̶l̶ ̶t̶h̶r̶e̶a̶d̶ ̶d̶u̶e̶ ̶t̶o̶ ̶i̶n̶f̶i̶n̶i̶t̶e̶ ̶w̶h̶i̶l̶e̶ ̶l̶o̶o̶p̶
        //	 call this function from the others each time something happens. pixels only affect themselvs 
        //-----------------------------------------------------------------------------
        static void sendFrameToConsole()
        {
            Console.Clear();
            for (byte i = 0; i < MAINGRID_X; i++)  // unsgined chars smaller then int
            {
                for (byte j = 0; j < MAINGRID_Y; j++)
                {
                    Console.Write(g_cmMainGrid[i, j]);
                }
                Console.WriteLine();
            }
        }

        //-----------------------------------------------------------------------------
        // Vertex
        //-----------------------------------------------------------------------------				
        public class Vertex
        {
            public Vertex m_pNext; // tell pawn next vertex in path // operator overload =
            public int m_iLocX;
            public int m_iLocY;

            //-----------------------------------------------------------------------------
            // Constructor
            //-----------------------------------------------------------------------------
            public Vertex(int iLocX, int iLocY)
            {
                setPosition(iLocX, iLocY);
            }

            //-----------------------------------------------------------------------------
            // setPosition
            //-----------------------------------------------------------------------------
            public void setPosition(int iLocX, int iLocY)
            {
                m_iLocX = iLocX;
                m_iLocY = iLocY;
            }


        }

        //-----------------------------------------------------------------------------
        // SubGrid
        //-----------------------------------------------------------------------------				
        public class SubGrid : Vertex
        {

            public char[,] m_cmGrid = new char[32, 32];  // dummy place holder 
            public int m_iSizeY;
            public int m_iSizeX;
            List<SubGrid> vSmartObject = new List<SubGrid>();// - Photoshop styled smart objects

            //-----------------------------------------------------------------------------
            // Constructor
            //-----------------------------------------------------------------------------
            public SubGrid(int iSizeY, int iSizeX, int iLocX, int iLocY) : base(iLocX, iLocY)
            {
                m_cmGrid = new char[iSizeX, iSizeY];   //constructor out of scope death? 
                m_iSizeY = iSizeY;
                m_iSizeX = iSizeX;
            }

            //-----------------------------------------------------------------------------
            // getSizeAsObject
            //-----------------------------------------------------------------------------		
            public MapVertex getSizeAsObject()
            {
                MapVertex mapVertex = new MapVertex(m_iSizeX, m_iSizeY);
                return mapVertex;
            }

            //-----------------------------------------------------------------------------
            // addSmartObject
            //  - Photoshop styled smart objects
            //-----------------------------------------------------------------------------		
            public void addSmartObject(int iSizeY, int iSizeX, int iLocX, int iLocY)
            {
                SubGrid subGrid = new SubGrid(iSizeY, iSizeX, iLocX, iLocY);
                vSmartObject.Add(subGrid);
            }



            //-----------------------------------------------------------------------------
            // wipeMatrix
            //    -heavy, use sparingly  O(n²)
            //-----------------------------------------------------------------------------
            public void wipeMatrix(char cFillWith)  // <-- discount memset substitute
            {
                //memset(cmGrid, ' ', (GRIDSIZE_Y * GRIDSIZE_X));
                for (int j = 0; j < m_iSizeX; j++)
                {
                    for (int i = 0; i < m_iSizeY; i++)
                    {
                        m_cmGrid[j, i] = cFillWith;
                    }
                }
            }

            //-----------------------------------------------------------------------------
            // setPixel
            //-----------------------------------------------------------------------------		
            public void setPixel(int iLocX, int iLocY, char cSymbol)
            {
                m_cmGrid[iLocY, iLocY] = cSymbol;
            }

            //-----------------------------------------------------------------------------
            // setPixelMap
            //-----------------------------------------------------------------------------		
            public void setPixelMap(MapVertex mapVertex, char cSymbol)
            {
                m_cmGrid[mapVertex.m_iX, mapVertex.m_iY] = cSymbol;
            }

            //-----------------------------------------------------------------------------
            // sendToGlobalBuffer
            //-----------------------------------------------------------------------------		
            public void sendToGlobalBuffer(int iLocX = -1, int iLocY = -1)
            {
                if (iLocX == -1) iLocX = m_iLocX;  // <-- Substitude for not allowing member variables in function call
                if (iLocY == -1) iLocY = m_iLocY;  // <-- Substitude for not allowing member variables in function call


                for (int i = 0; i < m_iSizeX; i++)
                {
                    for (int j = 0; j < m_iSizeY; j++)
                    {
                        g_cmMainGrid[iLocX + i, iLocY + j] = m_cmGrid[i, j];
                    }
                }
            }

            //-----------------------------------------------------------------------------
            // sendTextToBuffer
            //      -the iLocX & iLocY ofsets are local. They dont care about the global matrix
            //      -Spawns in the top-left corner unless other loc is specified
            //-----------------------------------------------------------------------------		
            public void sendTextToBuffer(string str, int iLocX = 0, int iLocY = 0)
            {
                //char caContent = str.ToCharArray;
                // overwrite matrix buffer start with tokenized string 
                for (int i = 0; i < str.Length; i++)
                {
                    m_cmGrid[iLocX, iLocY + i] = str[i];
                }
            }
        }

        //-----------------------------------------------------------------------------
        // diceStrenghtWidget
        //-----------------------------------------------------------------------------				
        public class diceWidget : SubGrid
        {

            private bool m_bActive = true;
            private int m_iDiceSeed = 0;
                //-----------------------------------------------------------------------------
            // Constructor
            //-----------------------------------------------------------------------------
            public diceWidget(int iSizeY, int iSizeX, int iLocX, int iLocY) : base(iSizeY, iSizeX, iLocX, iLocY)
            {
                m_cmGrid = new char[iSizeX, iSizeY];
                m_iSizeY = iSizeY;
                m_iSizeX = iSizeX;


                m_cmGrid[0, 0] = '[';
                m_cmGrid[0, m_iSizeY-1] = ']';
                         // (-1, array index)
            }

            //-----------------------------------------------------------------------------
            // deactivate
            //-----------------------------------------------------------------------------		
            public void deactivate()
            {
                m_bActive = false;
            }

            //-----------------------------------------------------------------------------
            // activate
            //-----------------------------------------------------------------------------		
            public /*async Task*/ void activate()
            {
                #region solely animation 
                //while (true)
                //{
                //    for (int i = 1; i < m_iSizeY-1; i++)
                //    {
                //        Thread.Sleep(50);
                //        m_cmGrid[0, i] = (char)127;
                //        sendToGlobalBuffer();
                //    }
                //    for (int i = m_iSizeY-2; i != 1; i--)
                //    {
                //        Thread.Sleep(50);
                //        m_cmGrid[0, i] = ' ';
                //        sendToGlobalBuffer();
                //
                //    }
                //}
                #endregion
                m_bActive = true;
                while (m_bActive)
                {
                    for (m_iDiceSeed = 1; m_iDiceSeed < m_iSizeY - 1; m_iDiceSeed++)
                    {
                        Thread.Sleep(40);
                        m_cmGrid[0, m_iDiceSeed] = (char)127;
                        sendToGlobalBuffer();

                        m_cmGrid[0, (m_iSizeY / 2)] = '|';

                        if (Console.KeyAvailable){  m_bActive = false;    break;}

                    }

                    if (Console.KeyAvailable) { m_bActive = false; break; }

                    for (m_iDiceSeed = m_iSizeY - 2; m_iDiceSeed != 1; m_iDiceSeed--)
                    {
                        Thread.Sleep(40);
                        m_cmGrid[0, m_iDiceSeed] = ' ';
                        sendToGlobalBuffer();

                        m_cmGrid[0, (m_iSizeY / 2)] = '|';

                        if (Console.KeyAvailable) { m_bActive = false; break; }

                    }
                }

            }

            //-----------------------------------------------------------------------------
            // getDiceSeed
            //-----------------------------------------------------------------------------		
            public int getDiceSeed()
            {

                //int iTemp = m_iDiceSeed % 6

                return m_iDiceSeed;
            }

        }

        public static void printVector()
        {
            while (true)
            {
                Console.Clear();

                for (int i = 0; i < g_vCachedPixelBuffer.Count; i++)
                {
                    //g_mutex.Close();
                    Console.WriteLine(g_vCachedPixelBuffer[i]);
                    //g_mutex.ReleaseMutex();
                }
                Thread.Sleep(500);

                //g_vCachedPixelBuffer.Clear();
            }
        }

        static List<string> g_vCachedPixelBuffer = new List<string>();

        //-----------------------------------------------------------------------------
        // finalize
        //-----------------------------------------------------------------------------		
        static public void finalize()
        {
            while (true)
            {
                Thread.Sleep(500);
                prepareGlobalSendFrameToConsoleStrBuilder();
                printVector();
            }
        }

        //-----------------------------------------------------------------------------
        // Main
        //
        //   Only draw one edge and flip due to symmetry?
        //       https://youtu.be/6nUMiJfHLSA
        //------------------------------------------------------------------------------				
        static void Main(string[] args)
        {

            initializeFrameBuffer(' '); // M is the widest, this creates the most symmetrical square

            Console.WriteLine("Loading...");
            //List<string> vCachedPixelBuffer = new List<string>();


            //SubGrid squarePopup = new SubGrid(42, 42,42,42);
            //squarePopup.wipeMatrix('c');
            //squarePopup.sendToGlobalBuffer();


            //SubGrid rectangelPopup = new SubGrid(64, 32, 60, 30);
            //rectangelPopup.wipeMatrix('X');
            //rectangelPopup.sendToGlobalBuffer();

            SubGrid uiGrid = new SubGrid(64, 6, 1, 0);
            uiGrid.wipeMatrix(' ');

            uiGrid.sendTextToBuffer("Name1",            0, 0);
            uiGrid.sendTextToBuffer("Name2",            0, 14);
            uiGrid.sendTextToBuffer("Name3",            0, 28);
            uiGrid.sendTextToBuffer("Name4",            0, 42);

            uiGrid.sendTextToBuffer("Color: Red",       1, 0);
            uiGrid.sendTextToBuffer("Color: Green",     1, 14);
            uiGrid.sendTextToBuffer("Color: Blue",      1, 28);
            uiGrid.sendTextToBuffer("Color: Yellow",    1, 42);

            uiGrid.sendTextToBuffer("Pawns: 1/4",       2, 0);
            uiGrid.sendTextToBuffer("Pawns: 2/4",       2, 14);
            uiGrid.sendTextToBuffer("Pawns: 3/4",       2, 28);
            uiGrid.sendTextToBuffer("Pawns: 4/4",       2, 42);

            //uiGrid.wipeMatrix('U');
            uiGrid.sendToGlobalBuffer();

            SubGrid levelGrid = new SubGrid(16, 11, 4, 0);
            //levelGrid.wipeMatrix('P');

            loadMapFromFile(levelGrid.getSizeAsObject());

            //StartMenu.Menu();
            

            for (int i = 0; i < g_vPosition.Count; i++)
            {
                levelGrid.setPixelMap(g_vPosition[i], '.');
            }
            levelGrid.setPixelMap(g_vPosition[21], 'G');
            levelGrid.sendToGlobalBuffer();

            

            diceWidget diceWidget = new diceWidget(16, 1, 0, 0);
            diceWidget.sendToGlobalBuffer();

            Thread thread = new Thread(diceWidget.activate);
            thread.Start();

            //Thread thread = new Thread(finalize);
            //thread.Start();

            //Thread renderThread = new Thread(printVector);
            //renderThread.Start();

            //printVector();
            while (true)
            {
                sendFrameToConsole();
            }
            //while (true)
            //{
            //Console.Clear();
            //sendFrameToConsole();            //⚠ TODO: Not sustainable performance! need acces to console buffer	⚠		

            //Thread.Sleep(500);
            //}
        }

    }
}