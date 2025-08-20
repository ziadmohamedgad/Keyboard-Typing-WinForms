# ⌨️ Typing Tutor Application

A Windows Forms **Typing Tutor** written in C#.  
This app helps users practice typing skills with real-time feedback, accuracy tracking, sound effects, and customizable themes.

---

## ✨ Features

- **Typing Practice**
  - Displays a typing paragraph and checks keystrokes in real time.
  - Tracks **correct characters**, **wrong characters**, and **corrections**.
  - Shows accuracy and progress with a visual circle indicator.

- **Themes**
  - Multiple built-in themes: **RGB**, **Colorful**, **Standard**.
  - Multiple built-in backgrounds: **Light**, **Purple**, **Pink**, **Dark**.
  - Full UI theming (form, panels, labels, keyboard).

- **Keyboard Visualization**
  - On-screen keyboard highlights keys as you type.
  - Visual feedback for correct and incorrect keystrokes.

- **Sound Effects**
  - Configurable typing sounds (key press, wrong key).
  - Toggle sound options in settings.

- **Statistics Tracking**
  - Calculates speed and accuracy.
  - Displays typing results at the end of a session.

- **Panel & Options System**
  - Organized panels for results, settings, and the virtual keyboard.
  - Option buttons change appearance based on the active theme.
  - Option to change the training lesson. 

- **RGB Theme Animation**
  - Smooth color transitions with timers for a dynamic look.

---

##📄 License

This project is open-source.
Feel free to use and modify it for learning or personal use.

---

## 🔮 Future Improvements

Planned or possible enhancements:

- Refactor `MainForm.cs` into separate classes (`ThemeManager`, `SoundManager`, `TypingSession`, etc.) for better maintainability.
- Optimize text coloring in the `RichTextBox` for performance on long paragraphs.
- Centralize theme configuration to simplify adding new themes.
- Replace repetitive button image switch logic with dictionary-based mapping.
- Add user profiles and save typing history/statistics.
- Support custom paragraphs or imported text files for practice.
- Add more keyboard layouts (e.g., QWERTZ, AZERTY).
- Enhancing the RGB keyboard theme for better performance.
