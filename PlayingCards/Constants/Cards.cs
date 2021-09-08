using PlayingCards.Models;
using System.Collections.Generic;

namespace PlayingCards.Constants
{
    public static class Cards
    {
        public static readonly string[] RANKS = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        public static readonly int[] VALUES = new[] { 14, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public static readonly string[] SUITS = new[] { "♠", "♥", "♣", "♦" };
        public static readonly string[] SUIT_NAMES = new[] { "spade", "heart", "club", "diamond" };
        public static readonly Dictionary<int, SuitPosition[]> SUIT_POSITIONS = new Dictionary<int, SuitPosition[]>
        {
            { 0, new SuitPosition[] { new SuitPosition(0, 0, false) } }, // A
            { 1, new SuitPosition[] {
                new SuitPosition(0, -1, false),
                new SuitPosition(0, 1, true)
            } }, // 2
            { 2, new SuitPosition[] {
                new SuitPosition(0, -1, false),
                new SuitPosition(0, 0, false),
                new SuitPosition(0, 1, true)
            } }, // 3
            { 3, new SuitPosition[] {
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 4
            { 4, new SuitPosition[] {
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(0, 0, false),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 5
            { 5, new SuitPosition[] { 
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(-1, 0, false),
                new SuitPosition(1, 0, false),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 6
            { 6, new SuitPosition[] {
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(0, -0.5, false),
                new SuitPosition(-1, 0, false),
                new SuitPosition(1, 0, false),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 7
            { 7, new SuitPosition[] { 
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(0, -0.5, false),
                new SuitPosition(-1, 0, false),
                new SuitPosition(1, 0, false),
                new SuitPosition(0, 0.5, true),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 8
            { 8, new SuitPosition[] { 
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(-1, -0.33, false),
                new SuitPosition(1, -0.33, false),
                new SuitPosition(0, 0, false),
                new SuitPosition(-1, 0.33, true),
                new SuitPosition(1, 0.33, true),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 9
            { 9, new SuitPosition[] { 
                new SuitPosition(-1, -1, false),
                new SuitPosition(1, -1, false),
                new SuitPosition(0, -0.66, false),
                new SuitPosition(-1, -0.33, false),
                new SuitPosition(1, -0.33, false),
                new SuitPosition(-1, 0.33, true),
                new SuitPosition(1, 0.33, true),
                new SuitPosition(0, 0.66, true),
                new SuitPosition(-1, 1, true),
                new SuitPosition(1, 1, true)
            } }, // 10
            { 10, new SuitPosition[] {
                new SuitPosition(0, 0, false)
            } }, // J
            { 11, new SuitPosition[] {
                new SuitPosition(0, 0, false)
            } }, // Q
            { 12, new SuitPosition[] {
                new SuitPosition(0, 0, false)
            } }, // K
        };
    }
}