namespace Schedule
{
    /// <summary>
    /// Make code a little bit easier with this class
    /// </summary>
    sealed class BuffedButton : System.Windows.Controls.Button
    {
        /// <summary>
        /// Set's the line parametr
        /// </summary>
        /// <param name="line"></param>
        public BuffedButton(string line)
        {
            this.line = line;
        }
        /// <summary>
        /// The current line of the Transport
        /// </summary>
        public readonly string line;
    }
}
