@echo off
title Batch Script redémarrage serveur distant

shutdown /r /m \\%1

exit