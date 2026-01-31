namespace XrayOldworldLauncher.Models.Shared;

public static class ZoneIcons
{
    public static string Radiation = """
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-5 -5 110 110" width="24" height="24">
            <!-- Outer circle with worn effect -->
            <circle cx="50" cy="50" r="45" fill="none" stroke="#6B7556" stroke-width="2" opacity="0.6"/>
            <circle cx="50" cy="50" r="45" fill="none" stroke="#6B7556" stroke-width="1.5" opacity="0.3" stroke-dasharray="5,3"/>
            
            <!-- Center circle -->
            <circle cx="50" cy="50" r="8" fill="#D4A574"/>
            <circle cx="50" cy="50" r="6" fill="#1A1A16"/>
            
            <!-- Radiation blades -->
            <g id="blade">
                <path d="M 50 50 L 38 20 A 32 32 0 0 1 62 20 Z" fill="#D4A574" opacity="0.85"/>
                <path d="M 50 50 L 40 22 A 30 30 0 0 1 60 22 Z" fill="#CC8844" opacity="0.5"/>
            </g>
            <use href="#blade" transform="rotate(120 50 50)"/>
            <use href="#blade" transform="rotate(240 50 50)"/>
            
            <!-- Weathering overlay -->
            <circle cx="50" cy="50" r="45" fill="none" stroke="#3A3A32" stroke-width="0.5" opacity="0.3" stroke-dasharray="2,4"/>
        </svg>
    """;

    public static string Video = """
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-5 -5 110 110" width="24" height="24">
            <!-- Monitor body -->
            <rect x="15" y="20" width="70" height="50" rx="2" fill="#2D2D28" stroke="#6B7556" stroke-width="2"/>
            <rect x="18" y="23" width="64" height="44" fill="#1A1A16"/>
            
            <!-- Screen glow -->
            <rect x="20" y="25" width="60" height="40" fill="#4A6B72" opacity="0.15"/>
            
            <!-- Scan lines -->
            <g opacity="0.4">
                <line x1="20" y1="27" x2="80" y2="27" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="31" x2="80" y2="31" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="35" x2="80" y2="35" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="39" x2="80" y2="39" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="43" x2="80" y2="43" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="47" x2="80" y2="47" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="51" x2="80" y2="51" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="55" x2="80" y2="55" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="59" x2="80" y2="59" stroke="#D4D4C8" stroke-width="0.5"/>
                <line x1="20" y1="63" x2="80" y2="63" stroke="#D4D4C8" stroke-width="0.5"/>
            </g>
            
            <!-- Monitor stand -->
            <rect x="42" y="70" width="16" height="8" fill="#3A3A32" stroke="#6B7556" stroke-width="1.5"/>
            <rect x="35" y="78" width="30" height="3" rx="1" fill="#2D2D28" stroke="#6B7556" stroke-width="1.5"/>
            
            <!-- Control buttons -->
            <circle cx="78" cy="75" r="2" fill="#CC8844" opacity="0.6"/>
            <circle cx="72" cy="75" r="2" fill="#6B7556" opacity="0.6"/>
        </svg>
    """;

    public static string Sound = """
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-5 -5 110 110" width="24" height="24">
            <!-- Background circle -->
            <circle cx="50" cy="50" r="42" fill="none" stroke="#3A3A32" stroke-width="1.5" opacity="0.4"/>
            
            <!-- Waveform -->
            <path d="M 20 50 L 25 45 L 30 35 L 35 42 L 40 50 L 45 58 L 50 65 L 55 58 L 60 50 L 65 42 L 70 35 L 75 45 L 80 50" 
                  fill="none" stroke="#D4A574" stroke-width="2.5" stroke-linecap="round" opacity="0.85"/>
            <path d="M 20 50 L 25 47 L 30 40 L 35 45 L 40 50 L 45 55 L 50 60 L 55 55 L 60 50 L 65 45 L 70 40 L 75 47 L 80 50" 
                  fill="none" stroke="#CC8844" stroke-width="1.5" stroke-linecap="round" opacity="0.5"/>
            
            <!-- Speaker lines (left) -->
            <g opacity="0.6">
                <path d="M 15 40 Q 12 45 12 50 Q 12 55 15 60" fill="none" stroke="#6B7556" stroke-width="1.5"/>
                <path d="M 10 35 Q 6 42 6 50 Q 6 58 10 65" fill="none" stroke="#6B7556" stroke-width="1.5"/>
            </g>
            
            <!-- Speaker lines (right) -->
            <g opacity="0.6">
                <path d="M 85 40 Q 88 45 88 50 Q 88 55 85 60" fill="none" stroke="#6B7556" stroke-width="1.5"/>
                <path d="M 90 35 Q 94 42 94 50 Q 94 58 90 65" fill="none" stroke="#6B7556" stroke-width="1.5"/>
            </g>
            
            <!-- Static/noise effect -->
            <g opacity="0.2">
                <circle cx="28" cy="38" r="0.5" fill="#D4D4C8"/>
                <circle cx="52" cy="68" r="0.5" fill="#D4D4C8"/>
                <circle cx="72" cy="42" r="0.5" fill="#D4D4C8"/>
                <circle cx="45" cy="35" r="0.5" fill="#D4D4C8"/>
                <circle cx="65" cy="62" r="0.5" fill="#D4D4C8"/>
            </g>
        </svg>
    """;

    public static string Gameplay = """
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-5 -5 110 110" width="24" height="24">
            <!-- Detector body -->
            <rect x="35" y="25" width="30" height="45" rx="3" fill="#3A3A32" stroke="#6B7556" stroke-width="2"/>
            <rect x="37" y="27" width="26" height="41" rx="2" fill="#2D2D28"/>
            
            <!-- Screen -->
            <rect x="40" y="32" width="20" height="15" rx="1" fill="#1A1A16" stroke="#4A6B72" stroke-width="1"/>
            <rect x="41" y="33" width="18" height="13" fill="#4A6B72" opacity="0.2"/>
            
            <!-- Detection bars on screen -->
            <g opacity="0.7">
                <rect x="43" y="41" width="2" height="3" fill="#D4A574"/>
                <rect x="47" y="38" width="2" height="6" fill="#D4A574"/>
                <rect x="51" y="35" width="2" height="9" fill="#CC8844"/>
                <rect x="55" y="40" width="2" height="4" fill="#D4A574"/>
            </g>
            
            <!-- LED indicator -->
            <circle cx="56" cy="53" r="2.5" fill="#CC8844" opacity="0.8"/>
            <circle cx="56" cy="53" r="1.5" fill="#D4A574"/>
            
            <!-- Control buttons -->
            <rect x="42" y="56" width="7" height="4" rx="1" fill="#2D2D28" stroke="#6B7556" stroke-width="1"/>
            <rect x="51" y="56" width="7" height="4" rx="1" fill="#2D2D28" stroke="#6B7556" stroke-width="1"/>
            
            <!-- Antenna mount -->
            <rect x="48" y="23" width="4" height="4" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="1"/>
            
            <!-- Antenna -->
            <line x1="50" y1="23" x2="50" y2="12" stroke="#6B7556" stroke-width="2" stroke-linecap="round"/>
            <circle cx="50" cy="12" r="2" fill="#D4A574" opacity="0.6"/>
            
            <!-- Signal waves -->
            <g opacity="0.4">
                <path d="M 50 13 Q 45 16 42 20" fill="none" stroke="#CC8844" stroke-width="1"/>
                <path d="M 50 13 Q 55 16 58 20" fill="none" stroke="#CC8844" stroke-width="1"/>
                <path d="M 50 11 Q 43 15 38 22" fill="none" stroke="#D4A574" stroke-width="0.8"/>
                <path d="M 50 11 Q 57 15 62 22" fill="none" stroke="#D4A574" stroke-width="0.8"/>
            </g>
            
            <!-- Wear marks -->
            <line x1="38" y1="65" x2="42" y2="67" stroke="#1A1A16" stroke-width="0.5" opacity="0.3"/>
            <line x1="60" y1="30" x2="62" y2="34" stroke="#1A1A16" stroke-width="0.5" opacity="0.3"/>
        </svg>
    """;

    public static string Anomaly = """
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-5 -5 110 110" width="24" height="24">
            <!-- Background contaminated area -->
            <circle cx="50" cy="50" r="40" fill="#6B7556" opacity="0.1"/>
            
            <!-- Anomaly field particles -->
            <g id="anomaly">
                <circle cx="50" cy="30" r="3" fill="#4A6B72" opacity="0.6"/>
                <circle cx="50" cy="30" r="5" fill="none" stroke="#4A6B72" stroke-width="1" opacity="0.3"/>
                <circle cx="50" cy="30" r="7" fill="none" stroke="#4A6B72" stroke-width="0.5" opacity="0.2"/>
            </g>
            
            <use href="#anomaly" transform="translate(-15, 10) scale(0.8)"/>
            <use href="#anomaly" transform="translate(20, 5) scale(0.6)"/>
            <use href="#anomaly" transform="translate(-10, 30) scale(0.7)"/>
            <use href="#anomaly" transform="translate(15, 25) scale(0.9)"/>
            <use href="#anomaly" transform="translate(5, 35) scale(0.5)"/>
            
            <!-- Electro anomaly arcs -->
            <g opacity="0.5">
                <path d="M 35 40 Q 40 38 45 42 Q 48 45 52 43" fill="none" stroke="#D4A574" stroke-width="1.5" stroke-linecap="round"/>
                <path d="M 55 55 Q 58 52 62 54 Q 65 56 68 54" fill="none" stroke="#CC8844" stroke-width="1" stroke-linecap="round"/>
            </g>
            
            <!-- Warning boundary -->
            <circle cx="50" cy="50" r="38" fill="none" stroke="#CC8844" stroke-width="2" stroke-dasharray="4,6" opacity="0.6"/>
            
            <!-- Radiation markers -->
            <g opacity="0.4">
                <path d="M 25 25 L 27 25 L 26 22 Z" fill="#D4A574"/>
                <path d="M 75 30 L 77 30 L 76 27 Z" fill="#D4A574"/>
                <path d="M 70 72 L 72 72 L 71 69 Z" fill="#D4A574"/>
                <path d="M 30 75 L 32 75 L 31 72 Z" fill="#D4A574"/>
            </g>
            
            <!-- Energy distortion lines -->
            <g opacity="0.3">
                <path d="M 20 50 Q 30 45 40 50" fill="none" stroke="#6B7556" stroke-width="0.8"/>
                <path d="M 60 50 Q 70 55 80 50" fill="none" stroke="#6B7556" stroke-width="0.8"/>
                <path d="M 50 20 Q 45 30 50 40" fill="none" stroke="#6B7556" stroke-width="0.8"/>
                <path d="M 50 60 Q 55 70 50 80" fill="none" stroke="#6B7556" stroke-width="0.8"/>
            </g>
            
            <!-- Center danger marker -->
            <g transform="translate(50, 50)">
                <circle r="6" fill="#8B4A42" opacity="0.6"/>
                <path d="M -2 -3 L 0 3 L 2 -3 Z" fill="#D4D4C8" opacity="0.8"/>
                <circle cy="-1" r="0.8" fill="#D4D4C8"/>
            </g>
        </svg>
    """;

    public static string Controls = """
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="-5 -5 110 110" width="24" height="24">
            <!-- Keyboard section -->
            <g id="keyboard">
                <rect x="20" y="35" width="60" height="35" rx="2" fill="#2D2D28" stroke="#6B7556" stroke-width="2"/>
                
                <!-- Keys row 1 -->
                <rect x="25" y="40" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="33" y="40" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="41" y="40" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="49" y="40" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="57" y="40" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="65" y="40" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                
                <!-- Keys row 2 (WASD highlighted) -->
                <rect x="27" y="48" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="35" y="48" width="6" height="6" rx="1" fill="#D4A574" stroke="#CC8844" stroke-width="1" opacity="0.8"/>
                <rect x="43" y="48" width="6" height="6" rx="1" fill="#D4A574" stroke="#CC8844" stroke-width="1" opacity="0.8"/>
                <rect x="51" y="48" width="6" height="6" rx="1" fill="#D4A574" stroke="#CC8844" stroke-width="1" opacity="0.8"/>
                <rect x="59" y="48" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="67" y="48" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                
                <!-- Keys row 3 -->
                <rect x="25" y="56" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="33" y="56" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="41" y="56" width="6" height="6" rx="1" fill="#D4A574" stroke="#CC8844" stroke-width="1" opacity="0.8"/>
                <rect x="49" y="56" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="57" y="56" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                <rect x="65" y="56" width="6" height="6" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
                
                <!-- Space bar -->
                <rect x="33" y="64" width="34" height="4" rx="1" fill="#3A3A32" stroke="#6B7556" stroke-width="0.8"/>
            </g>
            
            <!-- Mouse cursor overlay -->
            <g transform="translate(70, 25)" opacity="0.7">
                <path d="M 0 0 L 0 12 L 3 9 L 5 13 L 7 12 L 5 8 L 9 8 Z" fill="#6B7556" stroke="#D4D4C8" stroke-width="0.8"/>
                <circle cx="2" cy="4" r="0.8" fill="#CC8844" opacity="0.6"/>
            </g>
            
            <!-- Gamepad hint (bottom corner) -->
            <g transform="translate(15, 75)" opacity="0.5">
                <circle cx="0" cy="0" r="5" fill="#3A3A32" stroke="#6B7556" stroke-width="1"/>
                <circle cx="0" cy="0" r="2" fill="#6B7556"/>
            </g>
            
            <!-- Connection indicator -->
            <g opacity="0.4">
                <line x1="50" y1="30" x2="50" y2="35" stroke="#4A6B72" stroke-width="1.5"/>
                <circle cx="50" cy="28" r="2" fill="#4A6B72"/>
            </g>
        </svg>
    """;
}