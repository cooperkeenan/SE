#!/usr/bin/env python3
import os
import sys

try:
    import pyperclip
except ImportError:
    print("The 'pyperclip' module is not installed. Install it using 'pip install pyperclip'")
    sys.exit(1)

def main():
    # Define the base directory that contains your MAUI app files.
    base_dir = "Notes"  # adjust this path if needed

    if not os.path.isdir(base_dir):
        print(f"Error: '{base_dir}' is not a valid directory.")
        sys.exit(1)

    aggregated_content = ""
    
    # Walk the directory tree recursively.
    for root, dirs, files in os.walk(base_dir):
        for filename in files:
            # Check if the file ends with .xaml or .xaml.cs
            if filename.endswith(".xaml") or filename.endswith(".xaml.cs"):
                filepath = os.path.join(root, filename)
                aggregated_content += f"----- {filepath} -----\n"
                try:
                    with open(filepath, 'r', encoding='utf-8') as file:
                        aggregated_content += file.read() + "\n\n"
                except Exception as e:
                    print(f"Error reading {filepath}: {e}", file=sys.stderr)
    
    if aggregated_content:
        # Copy the aggregated content to the clipboard.
        pyperclip.copy(aggregated_content)
        print("Successfully copied the contents of all .xaml and .xaml.cs files to the clipboard.")
    else:
        print("No .xaml or .xaml.cs files found in the directory.")

if __name__ == "__main__":
    main()
