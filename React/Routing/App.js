import "./styles.css";
import { Link, BrowserRouter as Router, Route ,Routes} from 'react-router-dom'
import Home from "./Home";
import Profile from "./Profile";
export default function App() {
  return (
    <div className="App">
    <Router>
     <Link to="/"> Profile</Link>|
   <Link to="/Home"> Home</Link>
    <Routes>
    <Route path="/" exact element={<Profile />} />
   <Route path="/Home" element={<Home />} />
   </Routes>
    </Router>
        </div>
  );
  
}
