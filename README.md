# FileSharing
Currently this app is setup to run on a local network

How to use:
1. Run Server.exe
2. Run "ipconfig" in Command Prompt
3. Copy and paste the IPv4 Address to the Server.exe prompt

4. Run FileSharing.exe (for best demo, run this on another machine that is on the same network, however, still works on a single machine just fine)
5. Paste the IPv4 Address into Server IP box, add ":25565" to the end (default port for now)
6. Press connect button

7. Toss some random files (smaller than 100MB) into the "Upload" folder in the "AppSever" on the client machine
8. Press "push" button (notice the files will now be uploaded to the "AppSever" folder on the host machine
9. Press the "pull" button (notice the server files will now be downloaded to the "Downloads" folder on the client machine's desktop

Please note this is a prototype that only allows for the core functionality with little to no error checking
