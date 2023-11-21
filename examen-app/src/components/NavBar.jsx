import React from 'react';
import { Link } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';

export default function ButtonAppBar() {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
           <Button color="inherit" ><Link to ="/">Juegos</Link></Button>
           <Button color='inherit'><Link to ="/Personaje">Personajes</Link></Button>
           <Button color='inherit'><Link to ="/Compañia">Compañias</Link></Button>

        </Toolbar>
      </AppBar>
    </Box>
  );
}